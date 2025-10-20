import { Component } from '@angular/core';
import {MatIcon} from '@angular/material/icon'
import {MatButton} from '@angular/material/button'
import {MatBadgeModule} from '@angular/material/badge';
@Component({
  selector: 'app-header',
  imports: [MatIcon,MatButton,MatBadgeModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

}
