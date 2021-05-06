import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import {CountryDTO} from '../shared/DTOs/CountryDTO';

@Component({
  selector: 'app-country-list',
  templateUrl: './country-list.component.html',
  styleUrls: ['./country-list.component.css']
})
export class CountryListComponent implements OnInit {

  @Input() countryList: CountryDTO[];
  @Output() countryIdEvent = new EventEmitter<number>();
  
  public SelectedCountryId = 0;
  control = new FormControl(0, Validators.required);
  constructor() {
    
  }

  ngOnInit(): void {
  }

  listChange(selectedValue){
      this.countryIdEvent.emit(selectedValue);
  }

}
