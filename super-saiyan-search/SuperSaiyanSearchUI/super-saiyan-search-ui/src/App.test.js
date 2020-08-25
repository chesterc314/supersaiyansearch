import React from 'react';
import { render } from '@testing-library/react';
import App from './App';

test('renders Your Website link', () => {
  const { getByText } = render(<App />);
  const linkElement = getByText(/Your Website/i);
  expect(linkElement).toBeInTheDocument();
});
