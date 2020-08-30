import React, { useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import GridListTileBar from '@material-ui/core/GridListTileBar';
//import ListSubheader from '@material-ui/core/ListSubheader';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import { Typography, CircularProgress } from '@material-ui/core';
import Link from '@material-ui/core/Link';
import Fab from '@material-ui/core/Fab';

const useStyles = makeStyles((theme) => ({
    root: {
        display: 'flex',
        flexWrap: 'wrap',
        justifyContent: 'space-around',
        overflow: 'hidden',
        backgroundColor: theme.palette.background.paper,
    },
    search: {
        display: 'flex',
        justifyContent: 'space-around',
        paddingTop: '24px',
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
                    console.log(json);
                    return json;
                })
                .then(result => {
                    setProductResult(result);
                    setIsSearchClicked(false);
                }).catch(error => console.log('error', error));
        }
    };

    const handleButtonClick = () => {
        fetchProductsFromApi();
    };

    const handleKeyDown = (e) => {
        if (e.key === 'Enter') {
            fetchProductsFromApi();
        }
    }

    const resultsComponent = () => (<div className={classes.results}>
        {(productResult !== null) && <Typography component="h3">Total Results: {productResult.totalResults}</Typography>}
    </div>);
    
    return (
        <React.Fragment>
            <div className={classes.search}>
                {!isSearchClicked && <TextField id="search" label="Keyword" aria-label="Keyword" value={keyword} onChange={e => setKeyword(e.target.value)} onKeyDown={handleKeyDown} fullWidth required />}
                {(productResult === null && isSearchClicked) && <div className={classes.progress}><CircularProgress aria-label="loading" /></div>}
            </div>
            <div className={classes.searchButton}>
                {!isSearchClicked && <Button variant="contained" color="primary" aria-label="Search" onClick={handleButtonClick}>Search</Button>}
            </div>
            {resultsComponent()}
            <div className={classes.root}>
                <GridList cols={4} component="ul">
                    {(productResult !== null) && productResult.products.map((product) => (
                        <GridListTile key={product.name}>
                            <Link color="inherit" href={product.sourceUrl} target="_blank"><img src={product.imageUrl} alt={product.name} width="70%" height="80%" /></Link>
                            <GridListTileBar
                                title={product.name}
                                subtitle={
                                    <React.Fragment>
                                        <div>Price: {product.price}</div>
                                        <div>Source: {product.source}</div>
                                    </React.Fragment>
                                } />
                        </GridListTile>
                    ))}
                </GridList>
            </div>
            {resultsComponent()}
            {(productResult !== null) &&
                <div className={classes.compareButton}>
                    <Fab variant="extended" color="primary" aria-label="Compare">Compare</Fab>
                </div>}
        </React.Fragment>
    );
}
