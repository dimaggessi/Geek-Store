import {Pipe, PipeTransform} from '@angular/core';
import {AddressInterface} from '@shared/models/address.interface';

@Pipe({
  name: 'address',
  standalone: true,
})
export class AddressPipe implements PipeTransform {
  transform(value: AddressInterface, ...args: unknown[]): unknown {
    return `${value.street} nº ${value.number}, ${value.neighborhood}, ${value.city} - ${value.postalCode}`;
  }
}
