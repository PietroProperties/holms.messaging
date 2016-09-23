namespace HOLMS.Messaging.Topics {
    public class BookingContextTopics {
        public const string GroupBookingQuantities = "bookings.groups.quantities";

        public const string ReservationCancellation = "bookings.reservations.cancellations";
        public const string ReservationCheckInTopic = "booking.reservations.checkins";
        public const string ReservationFRPAmendment = "bookings.reservations.frp_amendment";
        public const string ReservationGuaranteedWithCheck = "bookings.reservations.guarantees.check";
        public const string ReservationAccruals = "bookings.reservations.accruals";
    }
}
