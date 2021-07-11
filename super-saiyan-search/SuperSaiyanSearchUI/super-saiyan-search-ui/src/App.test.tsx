import { render } from '@testing-library/react';
import App from './App';

test('renders Super Saiyan Search text', () => {
  const { getByText } = render(<App />);
  const text = getByText(/Super Saiyan Search/i);
  expect(text).toBeInTheDocument();
});
