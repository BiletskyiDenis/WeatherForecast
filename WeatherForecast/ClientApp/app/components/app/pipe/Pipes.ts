import {Pipe,PipeTransform} from "@angular/core"

@Pipe({ name: 'roundDegree' })
export class RoundDegreePipe implements PipeTransform {
    transform(input: number) {
        return Math.floor(input);
    }
}

@Pipe({ name: 'shortName' })
export class ShortNamePipe implements PipeTransform {
    transform(name: string) {
        return name.substring(0,3);
    }
}

@Pipe({ name: 'hhmm' })
export class TimePipe implements PipeTransform {
    transform(dateTime: number) {
        return new Date(dateTime * 1000).toString().split(" ")[4].substring(0, 5);
    }
}

@Pipe({ name: 'dd' })
export class DayPipe implements PipeTransform {
    transform(dateTime: number) {
        return new Date(dateTime * 1000).getDate();
    }
}

@Pipe({ name: 'ddName' })
export class DayNamePipe implements PipeTransform {
    transform(dateTime: number) {
        var weekday: string[] = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday","Saturday"];
        return weekday[new Date(dateTime * 1000).getDay()];
    }
}

@Pipe({ name: 'mmName' })
export class MonthNamePipe implements PipeTransform {
    transform(dateTime: number) {
        var month: string[] = ["January", "February", "March", "April", "May", "June",
                                  "July", "August", "September", "October", "November", "December"];
        return month[new Date(dateTime * 1000).getMonth()];
    }
}

@Pipe({ name: 'YY' })
export class YearNamePipe implements PipeTransform {
    transform(dateTime: number) {
        return new Date(dateTime * 1000).getFullYear();
    }
}