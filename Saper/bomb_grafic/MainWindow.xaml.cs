using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Security.AccessControl;
using Microsoft.VisualBasic;

namespace bomb_grafic {
	public partial class MainWindow : Window {

		bool IsVisable = false;

		static uint numberOfBombs = 10;
		static uint InGameNumberOfBombs = numberOfBombs;
		static uint TooMuchBombs = 0;
		
		Random rnd = new Random();
		
		DispatcherTimer timer = new DispatcherTimer();
		DateTime startOfGame = DateTime.MinValue;
		
		//to będzie można zmienić poprzez nowe okienko
		int dimensionX = 10;
		int dimensionY = 10;
		
		//domyślenie ustawienia planszy są 10 na 10
		short[,] Positions = new short[10, 10];
		Button[,] Buttons = new Button[10, 10];
		List<Button> checkedFields = new List<Button>();
		public MainWindow() {
			InitializeComponent();
		}
		private void GenerateBombs() {
			for(int x = 0; x < dimensionX; x++) {
				for(int y = 0; y < dimensionY; y++) {
					Positions[x, y] = 0;
				}
			}
			for(int bombs = 0; bombs < numberOfBombs; bombs++) {
				int x = rnd.Next(0, dimensionX);
				int y = rnd.Next(0, dimensionY);
				if(Positions[x, y] == 0) {
					Positions[x, y] = 10;
				}
				else { bombs--; }
			}
		}
		private void GeneratePositionValue() {
			byte bombCounter;
			for(int x = 0;x < dimensionX;x++) {
				for(int y = 0;y < dimensionY;y++) {
					//jeśli pole posiada bombe to skip żeby nie zmienic jej wartości
					if(Positions[x, y] == 10) { continue; }
					bombCounter = 0;
					
					for(int currentPositionX = -1;currentPositionX < 2;currentPositionX++) {						
						int checkX = x + currentPositionX;
						
						for(int currentPositionY = -1; currentPositionY < 2;currentPositionY++) {
							int checkY = y + currentPositionY;

							// jeśli będzie mniejsze od zera lub wieksze od wymiarów to wychodze poza właściwe wymiary planszy, zmienna nie będzie zliczać samej siebie pole [0,0]
							if(checkX < 0 || checkY < 0 || checkX >= dimensionX || checkY >= dimensionY) { continue; }
							if(checkX == x && checkY == y) { continue; }
							if(Positions[checkX, checkY] == 10) { bombCounter++; }
						}
					}
					// jeśli nie ma bomb w koło to pole przyjmuje wartość pustego pola
					if(bombCounter == 0) { Positions[x, y] = 11; }
					else { Positions[x, y] = bombCounter; }
				}
			}
		}
		private void GenerateButtons() {
			for(int x = 0; x < dimensionX; x++) {
				for(int y = 0; y < dimensionY; y++) {
					Button button = new Button();
					button.Name = $"Button_{x}_{y}";
					button.Width = (FieldCanvas.Width / dimensionX);
					button.Height = (FieldCanvas.Height / dimensionY);
					button.Click += Reveal_Click;
					button.MouseRightButtonDown += Flagged_Click;
					button.Tag = $"{x},{y}";
					//button.Content = $"{Positions[x, y]}"; // pole do testów
					button.Background = Brushes.Red;
					button.BorderBrush = Brushes.Red;
					Buttons[x, y] = button;
				}
			}
		}
		private void AddButtons() {
			if(!IsVisable) {
				for(int x = 0; x < dimensionX; x++) {
					for(int y = 0; y < dimensionY; y++) {
						Canvas.SetLeft(Buttons[x, y], x * Buttons[x, y].Width);
						Canvas.SetTop(Buttons[x, y], y * Buttons[x, y].Height);
						FieldCanvas.Children.Add(Buttons[x, y]);
					}
				}
				IsVisable = true;
			}
		}
		private void RemoveButtons() {
			if(IsVisable) {
				for(int x = 0; x < dimensionX; x++) {
					for(int y = 0; y < dimensionY; y++) {
						FieldCanvas.Children.Remove(Buttons[x, y]);
					}
				}
				IsVisable = false;
			}
		}
		private void Timer_Tick(object sender, EventArgs e) {
			TimeSpan dod = DateTime.Now - startOfGame;
			if(dod > TimeSpan.Zero) {
				Timer.Content = dod.ToString(@"mm\:ss");
			}
		}
		private void EndGameEventAppear(bool casecode) {
			if(casecode == false) {
				timer.Stop();
				for(int x = 0; x < dimensionX; x++) {
					for(int y = 0; y < dimensionY; y++) {
						Buttons[x, y].Content = Positions[x, y].ToString();
						Buttons[x, y].IsEnabled = false;
					}
				}
				MessageBox.Show("przegrałeś!");
			}
			else { 
				timer.Stop();
				for(int x = 0; x < dimensionX; x++) {
					for(int y = 0; y < dimensionY; y++) {
						Buttons[x, y].IsEnabled = false;
					}
				}
				MessageBox.Show("wygrałeś!");
			}
		}
		private void Reveal_Click(object sender, RoutedEventArgs e) {
			Button button = (Button) sender;
			int x = Convert.ToInt32(button.Tag.ToString().Split(',').GetValue(0));
			int y = Convert.ToInt32(button.Tag.ToString().Split(',').GetValue(1));
			//jeśli bomba 
			if(Positions[x, y] == 10) {
				button.Background = Brushes.DarkRed;
				EndGameEventAppear(false);
			}
			//jeśli pole puste
			else if(Positions[x, y] == 11 && button.Background != Brushes.Orange) {
				button.Background = Brushes.Green;
				button.BorderBrush = button.Background;
				button.Click -= Reveal_Click;
				button.MouseRightButtonDown -= Flagged_Click;
				checkedFields.Add(button);
				ReavealAdjacentEmptyFields(button);
				if(checkedFields.Count == (dimensionX * dimensionY) - InGameNumberOfBombs) {
					EndGameEventAppear(true);
				}
			}
			//jeśli pole z liczbą
			else {
				button.Background = Brushes.Green;
				button.BorderBrush = button.Background;
				button.Content = Positions[x, y];
				button.Click -= Reveal_Click;
				button.Click += Number_Click;
				button.MouseRightButtonDown -= Flagged_Click;
				checkedFields.Add(button);
				if(checkedFields.Count == (dimensionX * dimensionY) - InGameNumberOfBombs) {
					EndGameEventAppear(true);
				}
			}
		}
		private void ReavealAdjacentEmptyFields(Button button) {
			int x = Convert.ToInt32(button.Tag.ToString().Split(',').GetValue(0));
			int y = Convert.ToInt32(button.Tag.ToString().Split(',').GetValue(1));

			for(int currentPositionX = -1; currentPositionX < 2; currentPositionX++) {
				int checkX = x + currentPositionX;

				for(int currentPositionY = -1; currentPositionY < 2; currentPositionY++) {
					int checkY = y + currentPositionY;

					// jeśli będzie mniejsze od zera lub wieksze od wymiarów to wychodze poza właściwe wymiary planszy, zmienna nie będzie zliczać samej siebie pole [0,0]
					if(checkX < 0 || checkY < 0 || checkX >= dimensionX || checkY >= dimensionY) { continue; }
					if(checkX == x && checkY == y) { continue; }
					
					//wczytuje przycisk z macierzy
					Button adjButton = Buttons[checkX,checkY];
 
					int xAdj = Convert.ToInt32(adjButton.Tag.ToString().Split(',').GetValue(0));
					int yAdj = Convert.ToInt32(adjButton.Tag.ToString().Split(',').GetValue(1));
					
					//sprawdzam wczytany przycisk
					if(Positions[xAdj,yAdj] == 11 && adjButton.Background != Brushes.Orange) {
						if(adjButton.Background != Brushes.Green) {
							adjButton.Background = Brushes.Green;
							adjButton.BorderBrush = adjButton.Background;
							adjButton.Click -= Reveal_Click;
							adjButton.MouseRightButtonDown -= Flagged_Click;
							checkedFields.Add(adjButton);
							ReavealAdjacentEmptyFields(adjButton);
							if(checkedFields.Count == (dimensionX * dimensionY) - InGameNumberOfBombs) {
								EndGameEventAppear(true);
							}
						}
					}
					//jeśli pole z liczbą
					if(Positions[xAdj,yAdj]  != 10 && adjButton.Background != Brushes.Orange) { 
						if(adjButton.Background != Brushes.Green) {
							adjButton.Background = Brushes.Green;
							adjButton.BorderBrush = adjButton.Background;
							adjButton.Content = Positions[xAdj, yAdj].ToString();
							adjButton.Click -= Reveal_Click;
							adjButton.Click += Number_Click;
							adjButton.MouseRightButtonDown -= Flagged_Click;
							checkedFields.Add(adjButton);
							if(checkedFields.Count == (dimensionX * dimensionY) - InGameNumberOfBombs) {
								EndGameEventAppear(true);
							}
						}
					}
				}
			}
		}
		private void Flagged_Click(object sender, RoutedEventArgs e) {
			Button button = (Button)sender;
			if(button.Background != Brushes.Orange) {
				//XAML CODE -> <Image Width="40" Height="40" Name="imgDynamic"/>
				//var path = Path.Combine(Environment.CurrentDirectory, "Bilder", "D:\\pcz\\programowanie\\niskopoziomowy\\bomb_grafic\\obraz.jpg");
				//var uri = new Uri(path);
				//imgDynamic.Source = new BitmapImage(uri);
				int x = Convert.ToInt32(button.Tag.ToString().Split(',').GetValue(0));
				int y = Convert.ToInt32(button.Tag.ToString().Split(',').GetValue(1));
				if(Positions[x, y] == 10) {
					checkedFields.Add(button);
					InGameNumberOfBombs--;
					BombCounter.Content = "Bombs left:" + (InGameNumberOfBombs - TooMuchBombs).ToString();
					if(InGameNumberOfBombs == 0 && TooMuchBombs == 0) { 
						EndGameEventAppear(true); 
					}
				}
				else { 
					TooMuchBombs++;
					BombCounter.Content = "Bombs left:" + (InGameNumberOfBombs - TooMuchBombs).ToString();
				}
				button.Background = Brushes.Orange;
				button.Click -= Reveal_Click;
			}
			else {
				int x = Convert.ToInt32(button.Tag.ToString().Split(',').GetValue(0));
				int y = Convert.ToInt32(button.Tag.ToString().Split(',').GetValue(1));
				if(Positions[x, y] == 10) {
					checkedFields.Remove(button);
					InGameNumberOfBombs++;
					BombCounter.Content = "Bombs left:" + (InGameNumberOfBombs - TooMuchBombs).ToString();
				}
				else { 
					TooMuchBombs--;
					BombCounter.Content = "Bombs left:" + (InGameNumberOfBombs - TooMuchBombs).ToString();
					if(InGameNumberOfBombs == 0 && TooMuchBombs == 0) {
						EndGameEventAppear(true);
					}
				}
				button.Background = Brushes.Red;
				button.Click += Reveal_Click;
			}
		}
		private void Number_Click(object sender, RoutedEventArgs e) {
			Button button = (Button)sender;
			int flaggedBombCounter = 0;
			int x = Convert.ToInt32(button.Tag.ToString().Split(',').GetValue(0));
			int y = Convert.ToInt32(button.Tag.ToString().Split(',').GetValue(1));
			
			for(int currentPositionX = -1; currentPositionX < 2; currentPositionX++) {
				int checkX = x + currentPositionX;
				for(int currentPositionY = -1; currentPositionY < 2; currentPositionY++) {
					int checkY = y + currentPositionY;

					// jeśli będzie mniejsze od zera lub wieksze od wymiarów to wychodze poza właściwe wymiary planszy, zmienna nie będzie zliczać samej siebie pole [0,0]
					if(checkX < 0 || checkY < 0 || checkX >= dimensionX || checkY >= dimensionY) { continue; }
					if(checkX == x && checkY == y) { continue; }

					Button adjButton = Buttons[checkX, checkY];

					if(adjButton.Background == Brushes.Orange) { flaggedBombCounter++; }
				}
			}
			if(flaggedBombCounter == Positions[x, y]) {
				for(int currentPositionX = -1; currentPositionX < 2; currentPositionX++) {
					int checkX = x + currentPositionX;
					for(int currentPositionY = -1; currentPositionY < 2; currentPositionY++) {
						int checkY = y + currentPositionY;

						// jeśli będzie mniejsze od zera lub wieksze od wymiarów to wychodze poza właściwe wymiary planszy, zmienna nie będzie zliczać samej siebie pole [0,0]
						if(checkX < 0 || checkY < 0 || checkX >= dimensionX || checkY >= dimensionY) { continue; }
						if(checkX == x && checkY == y) { continue; }

						Button adjButton = Buttons[checkX, checkY];
						if(adjButton.Background != Brushes.Orange && adjButton.Background != Brushes.Green) {
							Reveal_Click(adjButton, e);
						}
					}
				}
				button.Click -= Number_Click;
			}
		}
		/// <summary>
		/// wartosć 10 oznacza pole z bombą -- pomarańczowy
		/// wartości 1-8 oznaczają ile bąmb okala dane pole
		/// 0 to pole bez ustalonej wartości
		/// 11 to pole bez bomb w około siebie
		/// </summary>
		private void startGame_Button_Click(object sender, RoutedEventArgs e) {
			//tworzenie planszy
			GenerateBombs();
			GeneratePositionValue();
			GenerateButtons();
			RemoveButtons();
			AddButtons();

			//ustawienie licznika bomb
			
			InGameNumberOfBombs = numberOfBombs;
			TooMuchBombs = 0;
			BombCounter.Content = "Bombs left:" + InGameNumberOfBombs.ToString();
			checkedFields = new List<Button>();

			//włączenie licznika czasu
			startOfGame = DateTime.Now;
			timer.Interval = TimeSpan.FromMilliseconds(10);
			timer.Tick += Timer_Tick;
			timer.Start();

		}
		private void SettingWindowCall_Click(object sender, RoutedEventArgs e) {
			timer.Stop();
			var newSettings = new Settings_windowxaml();
			var result = newSettings.ShowDialog();
			if (result == true) {
				RemoveButtons();
				// ustawianie nowych wartości 
				dimensionX = newSettings.Width;
				dimensionY = newSettings.Height;
				numberOfBombs = newSettings.Bombs;
				
				Positions = new short[dimensionX, dimensionY];
				Buttons = new Button[dimensionX, dimensionY];

			}
		}
	}
}
