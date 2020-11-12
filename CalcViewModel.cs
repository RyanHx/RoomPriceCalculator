using System;
using System.Collections.Generic;

namespace PriceCalc
{
    public class CalcViewModel : BaseViewModel
    {
        public List<string> WeekDays
        {
            get
            {
                return new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            }
        }
        public int CurrentWeekDay { get; set; }

        public List<String> Rooms
        {
            get
            {
                return new List<string> { "Room 1", "Room 2", "Room 3" };
            }
        }
        private int _currentRoomIndex = 0;
        public int CurrentRoomIndex
        {
            get { return _currentRoomIndex; }
            set
            {
                if(value == 0 && GuestsIndex > 5)
                {
                    GuestsIndex = 5;
                }
                _currentRoomIndex = value;
            }
        }

        private List<int> _numOfGuests;
        public List<int> NumOfGuests
        {
            get
            {
                _numOfGuests = CurrentRoomIndex == 0 ? new List<int> { 1, 2, 3, 4, 5, 6 } : new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
                return _numOfGuests;
            }
        }
        public int GuestsIndex { get; set; }

        public int AddGuests { get; set; }

        private double price => CalcPrice();
        public string Price => $"${Math.Truncate(price * 100) / 100}";

        private double taxPrice => CurrentWeekDay < 4 ? price : price + (price * 0.07);
        public string TaxPrice => $"${Math.Truncate(taxPrice * 100) / 100}";

        private double CalcPrice()
        {
            if(CurrentWeekDay < 4) return 99 + (AddGuests * 20);
            if (NumOfGuests[GuestsIndex] < 4) return 99 + (AddGuests * 20);
            double price = 28;
            if (CurrentRoomIndex == 0)
            {
                if (NumOfGuests[GuestsIndex] > 4)
                {
                    price *= 0.8;
                }
            }
            else
            {
                if (NumOfGuests[GuestsIndex] > 6)
                {
                    price *= 0.8;
                }
            }
            return price * (NumOfGuests[GuestsIndex] + AddGuests);
        }
    }
}
