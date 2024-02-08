import { Component } from '@angular/core';

@Component({
  selector: 'app-kalenteri',
  standalone: true,
  imports: [],
  templateUrl: './kalenteri.component.html',
  styleUrl: './kalenteri.component.css'
})
export class KalenteriComponent {
  daysOfWeek: string[] = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
  hoursOfDay: number[] = Array.from({ length: 10 }, (_, i) => i + 8); // 8:00 - 17:00
  calendar: any = {};

  reserveTime(day: number, hour: number, user: string): void {
    // Tässä voit toteuttaa logiikan varaamiseen
    console.log(`Varattu: Päivä ${day}, Kello ${hour}:00 - ${hour + 1}:00, Varaaja: ${user}`);
  }
}
