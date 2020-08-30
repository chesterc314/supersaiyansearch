import React, { useState } from 'react';
import { makeStyles, Typography, CircularProgress, GridList, GridListTile, GridListTileBar, Button, TextField, Link, Fab, Snackbar } from '@material-ui/core';

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
    compareButton: {
        display: 'flex',
        justifyContent: 'right',
    },
    progress: {
        display: 'flex',
        '& > * + *': {
            marginLeft: theme.spacing(2),
        },
    },
}));

export default function TitlebarGridList({ hostUrl }) {
    const [productResult, setProductResult] = useState(null);
    const [keyword, setKeyword] = useState(null);
    const [isSearchClicked, setIsSearchClicked] = useState(false);
    const [open, setOpen] = React.useState(false);
    const [message, setMessage] = React.useState("");
    const classes = useStyles();

    const fetchProductsFromApi = () => {
        if (keyword) {
            var requestOptions = {
                method: 'GET'
            };
            setProductResult(null);
            setIsSearchClicked(true);
            fetch(`${hostUrl}/api/products?q=${keyword}`, requestOptions)
                .then(response => {
                    const json = response.json();
                    return { json: json, status: response.status };
                })
                .then(result => {
                    setIsSearchClicked(false);
                    if (result.status === 200) {
                        setProductResult(result.json);
                    }
                    else if (result.status === 404) {
                        setMessage(`No products found for your search query: ${keyword}`);
                        setOpen(true);
                    }
                    else {
                        setMessage("An error occurred while processing your request.");
                        setOpen(true);
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

    const handleClose = (event, reason) => {
        if (reason === 'clickaway') {
            return;
        }
        setOpen(false);
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
            <div className={classes.root}>
                <GridList cols={4} component="ul">
                    {(productResult !== null) && productResult.products.map((product) => (
                        <GridListTile key={`${product.name}-${product.brand}`}>
                            <Link color="inherit" href={product.sourceUrl} target="_blank"><img src={product.imageUrl} alt={product.name} width="40%" height="80%" />
                                <GridListTileBar
                                    title={product.name}
                                    subtitle={
                                        <React.Fragment>
                                            <div>Price: R{product.price}</div>
                                            <div>Source: {product.source}</div>
                                        </React.Fragment>
                                    } />
                            </Link>
                        </GridListTile>
                    ))}
                </GridList>
            </div>
            {resultsComponent()}
            {(productResult !== null) &&
                <div className={classes.compareButton}>
                    <Fab variant="extended" color="primary" aria-label="Compare">Compare</Fab>
                </div>}
            <Snackbar anchorOrigin={{
                vertical: 'bottom',
                horizontal: 'center',
            }} open={open} message={<span>{message}</span>} autoHideDuration={6000} onClose={handleClose} />
        </React.Fragment>
    );
}
