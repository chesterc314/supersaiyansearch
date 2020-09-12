import React from 'react';
import Container from '@material-ui/core/Container';
import Typography from '@material-ui/core/Typography';
import SearchPage from './Pages/SearchPage';
import Settings from './settings.json';

function Copyright() {
  return (
    <Typography variant="body2" color="textSecondary" align="center">
      {'Copyright '}{'\u2122'}{' '}
        Super Saiyan Search
      {' '}
      {new Date().getFullYear()}
      {'.'}
    </Typography>
  );
}

export default function App() {
  return (
    <Container maxWidth="xl">
      <SearchPage hostUrl={Settings.hostUrl} isTest={Settings.isTest} />
      <Copyright />
      <a href="https://www.viglink.com/legal/consumer-disclosure/?vgtag=badge&vgref=459973">
        <img loading="lazy" title="Links monetized by VigLink" src="https://www.viglink.com/images/badges/88x31.png" alt="VigLink badge" width="88" height="31" />
      </a>
    </Container>
  );
}
