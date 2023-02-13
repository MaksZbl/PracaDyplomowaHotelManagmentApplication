export class PaymentDetail {
    payment_id: number;
    cardOwnerName: string;
    cardNumber: string;
    expirationDate: string;
    paymentDate: Date = new Date();
    securityCode: string;
    customer_id: number;
    booking_id: number;
}