export class Booking {
    booking_id: number;
    startDate: Date = new Date();
    endDate: Date = new Date();
    title: string = "New booking title";
    description: string = "New booking description";
    type: string = '';
}