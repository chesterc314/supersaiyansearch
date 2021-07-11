import { red } from '@material-ui/core/colors';
import { createTheme, Theme } from '@material-ui/core/styles';

const theme: Theme = createTheme({
  palette: {
    primary: {
      main: '#007bff',
    },
    secondary: {
      main: '#19857b',
    },
    error: {
      main: red.A400,
    },
    background: {
      default: '#fff',
    },
  },
});

export default theme;