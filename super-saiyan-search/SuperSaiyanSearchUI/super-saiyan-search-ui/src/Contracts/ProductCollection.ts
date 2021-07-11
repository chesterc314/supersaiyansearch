import Product from "./Product";

export default interface ProductCollection {
  products: Product[];
  totalResults: number;
}
