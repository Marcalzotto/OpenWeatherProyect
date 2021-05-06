import { NullableValuePipe } from './nullable-value.pipe';

describe('NullableValuePipePipe', () => {
  it('create an instance', () => {
    const pipe = new NullableValuePipe();
    expect(pipe).toBeTruthy();
  });
});
