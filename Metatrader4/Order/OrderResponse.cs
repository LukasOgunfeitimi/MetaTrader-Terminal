namespace Metatrader4.Order{
    class OrderResponse {
        public uint ticketNumber;
        public string asset;
        public int volume = 500;
        public byte direction = 0; // sell = 1, buy = 0, buylimit = 2, selllimit = 3, buystop = 4, sellstop = 5

        public string GetDirectionName() {
            return direction switch {
                0 => "buy",
                1 => "sell",
                2 => "buy limit",
                3 => "sell limit",
                4 => "buy stop",
                5 => "sell stop",
                _ => "unknown direction"
            };
        }
    }
}
