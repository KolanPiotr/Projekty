namespace Lab02.Views.Player {
    public class PlayerViewModel {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public PositionViewModel PositionViewModelTemplate { get; set; }
        public ICollection<PositionViewModel> Positions { get; set; }
    }
}
