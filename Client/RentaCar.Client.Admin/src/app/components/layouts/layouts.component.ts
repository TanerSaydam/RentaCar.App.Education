import { ChangeDetectionStrategy, Component, signal, ViewEncapsulation } from '@angular/core';
import { SharedModule } from '../../shared.module';
import { NavigationModel, navigations } from '../../navigation';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-layouts',
  standalone: true,
  imports: [SharedModule, RouterOutlet],
  templateUrl: './layouts.component.html',
  styleUrl: './layouts.component.css',
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class LayoutsComponent {
  navigations = signal<NavigationModel[]>(navigations);
}
