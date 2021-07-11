import { Typography, Container, Link } from "@material-ui/core";
import SearchPage from "./Pages/SearchPage";
import Settings from "./settings.json";

const Copyright = () => {
  return (
    <Typography variant="body2" color="textSecondary" align="center">
      {"Copyright "}
      {"\u2122"} Super Saiyan Search {new Date().getFullYear()}
      {"."}
      <OpenSourceLink />
    </Typography>
  );
};

const OpenSourceLink = () => {
  return (
    <div>
      <Link
        href="https://github.com/chesterc314/supersaiyansearch"
        target="_blank"
      >
        <img
          src="github.ico"
          alt="Github logo"
          width="64px"
          height="64px"
        ></img>
        <div>Source code</div>
      </Link>
    </div>
  );
};
const App = () => {
  return (
    <Container maxWidth="xl">
      <SearchPage hostUrl={Settings.hostUrl as string} />
      <Copyright />
    </Container>
  );
};

export default App;
