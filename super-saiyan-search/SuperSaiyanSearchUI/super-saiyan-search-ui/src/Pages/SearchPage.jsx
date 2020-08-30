import React, { useState } from 'react';
import {
    makeStyles,
    Typography,
    CircularProgress,
    GridList,
    GridListTile,
    GridListTileBar,
    Button,
    TextField,
    Link,
    Snackbar,
    Checkbox
} from '@material-ui/core';
import testPayload from './testpayload.json';

const useStyles = makeStyles((theme) => ({
    root: {
        display: 'flex',
        flexWrap: 'wrap',
        justifyContent: 'space-around',
        overflow: 'hidden',
        backgroundColor: theme.palette.background.paper,
    },
    logo: {
        display: 'flex',
        justifyContent: 'center',
        paddingTop: '24px',
    },
    search: {
        display: 'flex',
        justifyContent: 'space-around',
        paddingTop: '12px',
    },
    searchButton: {
        display: 'flex',
        justifyContent: 'center',
        paddingTop: '12px',
        paddingBottom: '12px',
    },
    results: {
        display: 'flex',
        justifyContent: 'center',
        paddingTop: '8px',
        paddingBottom: '8px',
    },
    difference: {
        display: 'flex',
        justifyContent: 'center',
        paddingTop: '8px',
        paddingBottom: '8px',
        position: 'sticky',
        top: '0',
    },
    progress: {
        display: 'flex',
        '& > * + *': {
            marginLeft: theme.spacing(2),
        },
    },
}));

export default function TitlebarGridList({ hostUrl, isTest }) {
    const [productResult, setProductResult] = useState(null);
    const [keyword, setKeyword] = useState(null);
    const [isSearchClicked, setIsSearchClicked] = useState(false);
    const [open, setOpen] = useState(false);
    const [message, setMessage] = useState("");
    const [product1, setProduct1] = useState(null);
    const [product2, setProduct2] = useState(null);
    const classes = useStyles();

    const fetchProductsFromApi = () => {
        if (keyword) {
            var requestOptions = {
                method: 'GET'
            };
            setProductResult(null);
            setIsSearchClicked(true);
            if (isTest) {
                setIsSearchClicked(false);
                setKeyword("");
                setProductResult(testPayload);
            } else {
                fetch(`${hostUrl}/api/products?q=${keyword}`, requestOptions)
                    .then(response => {
                        const json = response.json();
                        return { json: json, status: response.status };
                    })
                    .then(result => {
                        setIsSearchClicked(false);
                        setKeyword("");
                        if (result.status === 200) {
                            return result.json;
                        } else if (result.status === 404) {
                            setMessage(`No products found for your search query: ${keyword}`);
                            setOpen(true);
                        } else {
                            setMessage("An error occurred while processing your request.");
                            setOpen(true);
                        }
                    })
                    .then(result => {
                        if (result) {
                            setProductResult(result);
                        }
                    })
                    .catch(error => {
                        setIsSearchClicked(false);
                        if (error.toString().includes("NetworkError")) {
                            setMessage("Error connecting to the back-end server or not connected to the internet");
                        } else {
                            setMessage(`Unexpected error occurred: ${error}`);
                        }
                        setOpen(true);
                    });
            }
        }
    };

    const handleButtonClick = () => {
        fetchProductsFromApi();
    };

    const handleKeyDown = (e) => {
        if (e.key === 'Enter') {
            fetchProductsFromApi();
        }
    };

    const resultsComponent = () => (<div className={classes.results}>
        {(productResult !== null) && <Typography component="p">Total results: {productResult.totalResults}</Typography>}
    </div>);

    const differenceComponent = () => ((product1 && product2) && <div className={classes.difference}>
        {(productResult !== null) && <Typography component="p">Price difference: {(product1.price > product2.price) ? product1.price - product2.price : product2.price - product1.price}</Typography>}
    </div>);

    const handleClose = (event, reason) => {
        if (reason === 'clickaway') {
            return;
        }
        setOpen(false);
    };

    const handleChange = (event, product) => {
        if (event.target.checked) {
            setProduct1(product);
            if (!product2 && product !== product1) {
                setProduct2(product);
            }
        } else {
            if (product1) {
                setProduct1(null);
            }
            if (product2) {
                setProduct2(null);
            }
        }
    };

    return (
        <React.Fragment>
            <div className={classes.logo}>
                <img src="logo.png" width='15%' height='15%' alt="Super Saiyan Search Logo" />
            </div>
            <div className={classes.search}>
                {!isSearchClicked && <TextField id="search" label="Keyword" aria-label="Keyword" value={keyword !== null ? keyword : ""} onChange={e => setKeyword(e.target.value)} onKeyDown={handleKeyDown} fullWidth required />}
                {(productResult === null && isSearchClicked) && <div className={classes.progress}><CircularProgress aria-label="loading" /></div>}
            </div>
            <div className={classes.searchButton}>
                {!isSearchClicked && <Button variant="contained" color="primary" aria-label="Search" onClick={handleButtonClick}>Search</Button>}
            </div>
            {resultsComponent()}
            {differenceComponent()}
            <div className={classes.root}>
                <GridList cols={4} component="ul">
                    {(productResult !== null) && productResult.products.sort((a, b) => a.price - b.price)
                        .map((product, index) => (
                            <GridListTile key={`${product.brand}-${index}`}>
                                <Link color="inherit" href={product.sourceUrl} target="_blank">
                                    <img src={product.imageUrl} alt={product.name} width="45%" height="90%" />
                                    <GridListTileBar
                                        title={product.name}
                                        subtitle={
                                            <React.Fragment>
                                                <div>Price: R{product.price}</div>
                                                <div>Source: {product.source}</div>
                                            </React.Fragment>
                                        } />
                                </Link>
                                <Checkbox
                                    color="primary"
                                    checked={(product1 === product) ? true : (product2 === product) ? true : false}
                                    inputProps={{ 'aria-label': product.name }}
                                    onChange={(event) => handleChange(event, product)}
                                />
                            </GridListTile>
                        ))}
                </GridList>
            </div>
            {resultsComponent()}
            <Snackbar anchorOrigin={{
                vertical: 'bottom',
                horizontal: 'center',
            }} open={open} message={<span>{message}</span>} autoHideDuration={6000} onClose={handleClose} />
        </React.Fragment>
    );
}
