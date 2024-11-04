import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { SharedModule } from '../../shared.module';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class HomeComponent {

}
