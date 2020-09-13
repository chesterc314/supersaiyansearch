import React from 'react';
import { Typography, Container, Link } from '@material-ui/core';
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
      <Link href="https://www.viglink.com/legal/consumer-disclosure/?vgtag=badge&vgref=459973" align="center" target="_blank">
        <img title="Links monetized by VigLink" src="https://www.viglink.com/images/badges/88x31.png" alt="VigLink badge" width="88" height="31" />
      </Link>
    </Container>
  );
}
