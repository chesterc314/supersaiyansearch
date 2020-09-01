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
    </Container>
  );
}
