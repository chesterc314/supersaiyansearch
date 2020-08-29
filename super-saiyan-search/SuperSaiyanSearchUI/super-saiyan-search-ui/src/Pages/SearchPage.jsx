import React, { useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import GridListTileBar from '@material-ui/core/GridListTileBar';
import ListSubheader from '@material-ui/core/ListSubheader';
import IconButton from '@material-ui/core/IconButton';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
// import { InfoIcon } from '@material-ui/icons';
import { Typography } from '@material-ui/core';
import Link from '@material-ui/core/Link';

const useStyles = makeStyles((theme) => ({
    root: {
        display: 'flex',
        flexWrap: 'wrap',
        justifyContent: 'space-around',
        overflow: 'hidden',
        backgroundColor: theme.palette.background.paper,
    },
    icon: {
        color: 'rgba(255, 255, 255, 0.54)',
    },
}));

export default function TitlebarGridList() {
    const [productResult, setProductResult] = useState(null);
    const [keyword, setKeyword] = useState(null);
    const classes = useStyles();

    const fetchProducts = () => {
        var requestOptions = {
            method: 'GET',
            headers: {
                'Origin': 'https://localhost:5001'
            }
        };

        fetch(`https://localhost:5001/api/products?q=${keyword}`, requestOptions)
            .then(response => response.json())
            .then(result => setProductResult(result))
            .catch(error => console.log('error', error));
    }

    return (
        <div className={classes.root}>
            <Button variant="contained" color="primary" onClick={fetchProducts}>Search</Button>
            <TextField id="search" label="Keyword" value={keyword} onChange={e => setKeyword(e.target.value)} />
            {(productResult !== null) && <Typography variant="h2" component="h1">Total Results: {productResult.totalResults}</Typography>}
            <GridList cols={5} >
                {(productResult !== null) && productResult.products.map((product) => (
                    <GridListTile key={product.name}>
                        <img src={product.imageUrl} alt={product.name} />
                        <GridListTileBar
                            title={product.name}
                            subtitle={<span>Price: {product.price}</span>}
                            actionIcon={
                                <Link color="inherit" href={product.sourceUrl}>
                                    <IconButton aria-label={`Source: ${product.source}`} className={classes.icon}>
                                        {/* <InfoIcon /> */}
                                    </IconButton>
                                </Link>
                            }
                        />
                    </GridListTile>
                ))}
            </GridList>

        </div>
    );
}
