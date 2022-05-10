export function timeStringParser(timeString: string): { hours: number, minutes: number } {
    let timeStringTokens: string[] = timeString.split(':');
    let hours: number = parseInt(timeStringTokens[0], 10);
    let minutes: number = parseInt(timeStringTokens[1], 10);    
    return {hours: hours, minutes: minutes};
}

export function checkDatesEquality(date1: Date, date2: Date): boolean {
    date1.setHours(0, 0, 0, 0);
    date2.setHours(0, 0, 0, 0);

    if (date1.getTime() === date2.getTime()) {
        return true;
    }

    return false;
}