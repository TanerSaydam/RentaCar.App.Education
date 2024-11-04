import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { BlankComponent } from './components/blank/blank.component';
import {FlexiGridModule} from 'flexi-grid'
import { FlexiButtonComponent } from 'flexi-button';
import { FlexiPopupModule } from 'flexi-popup';
import { FlexiTooltipDirective } from 'flexi-tooltip';
import { FormsModule } from '@angular/forms';
import { NgxMaskDirective } from 'ngx-mask';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    BlankComponent, 
    FlexiGridModule,
    FlexiButtonComponent,
    FlexiPopupModule,
    FlexiTooltipDirective,
    FormsModule,
    NgxMaskDirective
  ],
  exports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    BlankComponent,
    FlexiGridModule,
    FlexiButtonComponent,
    FlexiPopupModule,
    FlexiTooltipDirective,
    FormsModule,
    NgxMaskDirective
  ]
})
export class SharedModule { }
