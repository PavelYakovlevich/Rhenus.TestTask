import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-users-dashboard',
  templateUrl: './users-dashboard.component.html',
  styleUrls: ['./users-dashboard.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UsersDashboardComponent {

}
