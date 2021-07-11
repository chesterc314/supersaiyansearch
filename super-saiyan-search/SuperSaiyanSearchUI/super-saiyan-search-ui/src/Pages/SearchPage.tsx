import React, { useState } from "react";
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
  Checkbox,
  Tooltip,
  Theme,
} from "@material-ui/core";
import InfoIcon from "@material-ui/icons/Info";
import ProductCollection from "../Contracts/ProductCollection";
import Product from "../Contracts/Product";

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    display: "flex",
    flexWrap: "wrap",
    justifyContent: "space-around",
    overflow: "hidden",
    backgroundColor: theme.palette.background.paper,
  },
  logo: {
    display: "flex",
    justifyContent: "center",
    paddingTop: "24px",
  },
  search: {
    display: "flex",
    justifyContent: "space-around",
    paddingTop: "12px",
  },
  searchButton: {
    display: "flex",
    justifyContent: "center",
    paddingTop: "12px",
    paddingBottom: "12px",
  },
  results: {
    display: "flex",
    justifyContent: "center",
    paddingTop: "8px",
    paddingBottom: "8px",
  },
  linkCheck: {
    display: "flex",
    justifyContent: "center",
  },
  difference: {
    display: "flex",
    justifyContent: "center",
    position: "sticky",
    top: 10,
    zIndex: 1,
    backgroundColor: theme.palette.primary.main,
    color: "#fff",
  },
  progress: {
    display: "flex",
    "& > * + *": {
      marginLeft: theme.spacing(2),
    },
  },
  icon: {
    color: "rgba(255, 255, 255, 0.54)",
  },
}));

interface SearchPageProps {
  hostUrl: string;
}

const SearchPage = (props: SearchPageProps) => {
  const { hostUrl } = props;
  const [productResult, setProductResult] = useState<ProductCollection | null>(
    null
  );
  const [keyword, setKeyword] = useState<string | null>(null);
  const [isSearchClicked, setIsSearchClicked] = useState(false);
  const [open, setOpen] = useState(false);
  const [message, setMessage] = useState("");
  const [product1, setProduct1] = useState<Product | null>(null);
  const [product2, setProduct2] = useState<Product | null>(null);
  const classes = useStyles();

  const fetchProductsFromApi = () => {
    if (keyword) {
      var requestOptions = {
        method: "GET",
      };
      setProductResult(null);
      setIsSearchClicked(true);
      setProduct1(null);
      setProduct2(null);
      fetch(`${hostUrl}/api/products?q=${keyword}`, requestOptions)
        .then((response) => {
          const json = response.json();
          return { json: json, status: response.status };
        })
        .then((result) => {
          setIsSearchClicked(false);
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
        .then((result) => {
          if (result) {
            setProductResult(result);
          }
        })
        .catch((error) => {
          setIsSearchClicked(false);
          if (error.toString().includes("NetworkError")) {
            setMessage(
              "Error connecting to the back-end server or not connected to the internet"
            );
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

  const handleKeyDown = (e: any) => {
    if (e.key === "Enter") {
      fetchProductsFromApi();
    }
  };

  const ProductResultsComponent = () => {
    return (
      <div className={classes.results}>
        {productResult !== null && (
          <Typography component="p">
            Total results: {productResult.totalResults}
          </Typography>
        )}
      </div>
    );
  };

  const ProductDifferenceComponent = () => {
    return (
      product1 &&
      product2 && (
        <div className={classes.difference}>
          {productResult !== null && (
            <Typography component="p" color="inherit">
              Price difference: R
              {product1.price > product2.price
                ? product1.price - product2.price
                : product2.price - product1.price}
            </Typography>
          )}
        </div>
      )
    );
  };

  const handleClose = (event: any, reason: string) => {
    if (reason === "clickaway") {
      return;
    }
    setOpen(false);
  };

  const handleChange = (event: any, product: Product) => {
    if (event.target.checked) {
      setProduct1(product);
      if (product2 === null && product !== product1) {
        setProduct2(product);
      }
    } else {
      setProduct1(null);
      setProduct2(null);
    }
  };

  return (
    <React.Fragment>
      <div className={classes.logo}>
        <img
          src="logo.png"
          width="25%"
          height="25%"
          alt="Super Saiyan Search Logo"
        />
      </div>
      <div className={classes.search}>
        {!isSearchClicked && (
          <TextField
            id="search"
            label="Keyword"
            aria-label="Keyword"
            value={keyword !== null ? keyword : ""}
            onChange={(e) => setKeyword(e.target.value)}
            onKeyDown={handleKeyDown}
            fullWidth
            required
          />
        )}
        {productResult === null && isSearchClicked && (
          <div className={classes.progress}>
            <CircularProgress aria-label="loading" />
          </div>
        )}
      </div>
      <div className={classes.searchButton}>
        {!isSearchClicked && (
          <Button
            variant="contained"
            color="primary"
            aria-label="Search"
            onClick={handleButtonClick}
          >
            Search
          </Button>
        )}
      </div>
      <ProductResultsComponent />
      <ProductDifferenceComponent />
      <div className={classes.root}>
        <GridList component="ul">
          {productResult !== null &&
            productResult.products.map((product, index) => (
              <GridListTile key={`${product.source}-${index}`}>
                <div className={classes.linkCheck}>
                  <Link
                    color="inherit"
                    href={product.sourceUrl}
                    target="_blank"
                  >
                    <img
                      src={product.imageUrl}
                      alt={product.name}
                      width="128px"
                      height="128px"
                    />
                  </Link>
                  <GridListTileBar
                    title={
                      <Link
                        color="inherit"
                        href={product.sourceUrl}
                        target="_blank"
                      >
                        {product.name}
                      </Link>
                    }
                    subtitle={
                      <React.Fragment>
                        <div>Price: R{product.price}</div>
                        <div>Source: {product.source}</div>
                      </React.Fragment>
                    }
                    actionIcon={
                      <Tooltip title={product.name} aria-label={product.name}>
                        <InfoIcon />
                      </Tooltip>
                    }
                  />
                  <Checkbox
                    color="primary"
                    checked={
                      product1 === product
                        ? true
                        : product2 === product
                        ? true
                        : false
                    }
                    inputProps={{ "aria-label": product.name }}
                    onChange={(event) => handleChange(event, product)}
                  />
                </div>
              </GridListTile>
            ))}
        </GridList>
      </div>
      <ProductResultsComponent />
      <Snackbar
        anchorOrigin={{
          vertical: "bottom",
          horizontal: "center",
        }}
        open={open}
        message={<span>{message}</span>}
        autoHideDuration={6000}
        onClose={handleClose}
      />
    </React.Fragment>
  );
};

export default SearchPage;
