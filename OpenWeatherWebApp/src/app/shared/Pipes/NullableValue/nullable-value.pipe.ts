import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'nullableValuePipe'
})
export class NullableValuePipe implements PipeTransform {

  transform(value: any): any {

    if(value === null || value === undefined)
      return 'N/A';
    else
      return value;
  }

}
