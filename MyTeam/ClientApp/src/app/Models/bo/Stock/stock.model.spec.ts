import { Stock } from './stock.model';

describe('Stock.Model', () => {
  it('should create an instance', () => {
    expect(new Stock()).toBeTruthy();
  });
});
