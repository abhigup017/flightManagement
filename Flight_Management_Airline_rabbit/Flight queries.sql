CREATE TABLE USERS(UserId INT identity(1,1), FirstName NVARCHAR(20) NOT NULL, LastName NVARCHAR(20) NOT NULL, GenderId INT NOT NULL , EmailId NVARCHAR(30) NOT NULL, RoleId INT NOT NULL, UserName NVARCHAR(50) NOT NULL, Password NVARCHAR(50) NOT NULL, PRIMARY KEY(UserId), foreign key(GenderId) references GENDERTYPE(GenderId), foreign key(RoleId) references ROLETYPE(RoleId))

CREATE TABLE GENDERTYPE(GenderId INT identity(1,1), GenderValue NVARCHAR(10) not null, Primary key(GenderId))

insert into GENDERTYPE(GenderValue) values ('Male'),('Female'),('Others')

create table ROLETYPE(RoleId int identity(1,1), RoleValue NVARCHAR(20), primary key(RoleId))

insert into ROLETYPE(RoleValue) values ('Admin'),('User')

create table AIRLINE
(
AirLineId int identity(1,1),
AirlineName NVARCHAR(20) not null,
AirlineLogo NVARCHAR(20) not null,
AirlineContact nvarchar(20) not null,
AirlineAddress NVARCHAR(100) not null,
AirlineDescription NVARCHAR(500),
IsBlocked bit default 0,
primary key(AirlineId)
)

create table LOCATIONS
(
LocationId int identity(1,1),
LocationName nvarchar(100) not null,
primary key(locationId)
)

create table MEALPLANS
(
MealPlanId int identity(1,1),
MealPlanType nvarchar(20) not null,
primary key(MealPlanId)
)

insert into MealPlans(MealPlanType)
values('Veg'),('Non-Veg'),('Veg & Non-Veg'),('None')

create table InstrumentTypes
(
InstrumentId int identity(1,1),
InstrumentName nvarchar(50) not null,
primary key(InstrumentId)
)

create table FLIGHTSCHEDULE
(
 FlightId int identity(1,1),
 FlightNumber Nvarchar(50) not null,
 AirLineId int not null,
 FlightDayScheduleId int not null,
 InstrumentId int not null,
 BusinessSeatsNo int not null,
 RegularSeatsNo int not null,
 VacantBusinessSeats int not null,
 VacantRegularSeats int not null,
 TicketCost decimal(18,2) not null,
 NoOfRows int not null,
 MealPlanId int not null,
 primary key(FlightId),
 foreign key(AirlineId) references AIRLINE(AirlineId),
 foreign key(FlightDayScheduleId) references FLIGHTDAYSSCHEDULE(FlightDayScheduleId),
 foreign key(InstrumentId) references InstrumentTypes(InstrumentId),
 foreign key(MealPlanId) references MEALPLANS(MealPlanId)
)

create table FLIGHTDAYSSCHEDULE
(
FlightDayScheduleId int identity(1,1),
--FlightId int not null,
SourceLocationId int not null,
DestinationLocationId int not null,
StartDateTime datetime not null,
EndDateTime datetime not null
primary key(FlightDayScheduleId),
foreign key(SourceLocationId) references LOCATIONS(LocationId),
foreign key(DestinationLocationId) references LOCATIONS(LocationId),
--foreign key(FlightId) references FLIGHTSCHEDULE(FlightId)
)

create table BOOKING
(
BookingId int identity(1,1),
FlightId int not null,
CustomerName nvarchar(50) not null,
CustomerEmailId nvarchar(50) not null,
NoOfSeats int not null,
--BookingPassengersId int not null default 0,
MealPlanId int not null,
PNRNumber nvarchar(100),
TravelDate datetime not null,
BookedOn datetime not null,
TotalCost decimal(18,2) not null,
IsCancelled bit default 0,
primary key(BookingId),
foreign key(MealPlanId) references MealPlans(MealPlanId)
)

create table BOOKINGPASSENGERS
(
PassengerId int identity(1,1),
BookingId int not null,
PassengerName nvarchar(50) not null,
GenderId int not null,
PassengerAge int not null,
SeatNo nvarchar(20),
IsBusinessSeat bit not null,
IsRegularSeat bit not null,
primary key(PassengerId),
Foreign key(BookingId) references BOOKING(BookingId)
)

create table DISCOUNT
(
DiscountId int identity(1,1),
DiscountCode nvarchar(50) not null,
DiscountValue int not null
primary key(DiscountId)
)

insert into LOCATIONS(LocationName) values
('Goa'),('Banglore'),('Delhi'),('Kolkata'),('Mumbai'),('Varanasi')

insert into InstrumentTypes(InstrumentName) values
('Airbus-A320'),('Airbus-A320 Neo'),('Boeing-777'),('Boeing-777 Max')