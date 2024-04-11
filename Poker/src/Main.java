import java.util.*;

public class Main {
    public static int handCompare(ArrayList<Player> players, int handRank) {
        int size = players.get(0).gethand().size();
        if (handRank == 6 || handRank == 1) { // kolor wysoka karta
            for (int i = size; i > 0; i--) {
                if (players.get(0).getCard(i - 1).getValueOfFigure() != players.get(1).getCard(i - 1).getValueOfFigure()) {
                    if (players.get(0).getCard(i - 1).getValueOfFigure() > players.get(1).getCard(i - 1).getValueOfFigure()) {
                        return 0;
                    } else {
                        return 1;
                    }
                }
            }
            if (players.get(0).getCard(size - 1).getValueOfColor() > players.get(1).getCard(size - 1).getValueOfColor()) {
                return 0;
            } else {
                return 1;
            }
        } else if (handRank == 5 || handRank == 9) { // strit poker
            if (players.get(0).getCard(size - 1).getValueOfFigure() > players.get(1).getCard(size - 1).getValueOfFigure()){
                if (players.get(0).getCard(size - 1).getValueOfFigure() == 14 && players.get(0).getCard(size - 2).getValueOfFigure() == 5){
                    return 1;
                } else {
                    return 0;
                }
            } else if (players.get(0).getCard(size - 1).getValueOfFigure() < players.get(1).getCard(size - 1).getValueOfFigure()) {
                return 1;
            } else {
                if (players.get(0).getCard(size - 1).getValueOfColor() > players.get(1).getCard(size - 1).getValueOfColor()){
                    return 0;
                } else {
                    return 1;
                }
            }
        } else if (handRank == 10) { // poker królewski
            if (players.get(0).getCard(0).getValueOfColor() > players.get(1).getCard(0).getValueOfColor()){
                return 0;
            } else {
                return 1;
            }
        } else if (handRank == 2 || handRank == 4 || handRank == 8) { // para, trójka, kareta
            if (players.get(0).getCard(Collections.max(players.get(0).getCombo())).getValueOfFigure() > players.get(1).getCard(Collections.max(players.get(1).getCombo())).getValueOfFigure()) {
                return 0;
            } else {
                if (players.get(0).getCard(Collections.max(players.get(0).getCombo())).getValueOfFigure() == players.get(1).getCard(Collections.max(players.get(1).getCombo())).getValueOfFigure()) {
                    for (int i = size - 1; i > -1; i--) {
                        for (int j = size - 1; j > -1; j--) {
                            if (!players.get(0).getCombo().contains(i) && !players.get(1).getCombo().contains(j)) {
                                if (players.get(0).getCard(i).getValueOfFigure() > players.get(1).getCard(j).getValueOfFigure()) {
                                    return 0;
                                } else if (players.get(0).getCard(i).getValueOfFigure() < players.get(1).getCard(j).getValueOfFigure()) {
                                    return 1;
                                }
                            }
                        }
                    }
//                    porównuje kolor pary która para ma wyższy kolor
                    if (players.get(0).getCard(Collections.max(players.get(0).getCombo())).getValueOfColor() > players.get(1).getCard(Collections.max(players.get(1).getCombo())).getValueOfColor()){
                        System.out.println("if4");
                        return 0;
                    } else {
                        return 1;
                    }
                } else {
                    return 1;
                }
            }
        } else if (handRank == 3) { // dwie pary
            System.out.println(Math.floorDiv(players.get(0).getCombo().size(), 2));
            if (players.get(0).getCard(players.get(0).getCombo().get(players.get(0).getCombo().size()-1)).getValueOfFigure() != players.get(1).getCard(players.get(1).getCombo().get(players.get(1).getCombo().size()-1)).getValueOfFigure()) {
                return players.get(0).getCard(players.get(0).getCombo().get(players.get(0).getCombo().size()-1)).getValueOfFigure() > players.get(1).getCard(players.get(1).getCombo().get(players.get(1).getCombo().size()-1)).getValueOfFigure() ? 0 : 1;
            } else {
                if (players.get(0).getCard(players.get(0).getCombo().get(players.get(0).getCombo().size()-3)).getValueOfFigure() != players.get(1).getCard(players.get(1).getCombo().get(players.get(1).getCombo().size()-3)).getValueOfFigure()) {
                    return players.get(0).getCard(players.get(0).getCombo().get(players.get(0).getCombo().size()-3)).getValueOfFigure() > players.get(1).getCard(players.get(1).getCombo().get(players.get(1).getCombo().size()-3)).getValueOfFigure() ? 0 : 1;
                } else {
                    for (int i = size - 1; i > -1; i--) {
                        for (int j = size - 1; j > -1; j--) {
                            if (!players.get(0).getCombo().contains(i) && !players.get(1).getCombo().contains(j)) {
                                if (players.get(0).getCard(i).getValueOfFigure() > players.get(1).getCard(j).getValueOfFigure()) {
                                    return 0;
                                } else if (players.get(0).getCard(i).getValueOfFigure() < players.get(1).getCard(j).getValueOfFigure()) {
                                    return 1;
                                }
                                break;
                            }
                        }
                    }
                    if (players.get(0).getCard(players.get(0).getCombo().size()-1).getValueOfColor() > players.get(1).getCard(players.get(0).getCombo().size()-1).getValueOfColor()){
                        return 0;
                    } else {
                        return 1;
                    }
                }
            }
        } else { // ful
            if (players.get(0).getCard(size - 1).getValueOfFigure() != players.get(1).getCard(size - 1).getValueOfFigure()){
                return players.get(0).getCard(size - 1).getValueOfFigure() > players.get(1).getCard(size - 1).getValueOfFigure() ? 0 : 1;
            } else {
                if (players.get(0).getCard(size - 4).getValueOfFigure() != players.get(1).getCard(size - 4).getValueOfFigure()){
                    return players.get(0).getCard(size - 4).getValueOfFigure() > players.get(1).getCard(size - 4).getValueOfFigure() ? 0 : 1;
                } else {
                    if (players.get(0).getCard(size - 1).getValueOfColor() > players.get(1).getCard(size - 1).getValueOfColor()){
                        return 0;
                    } else {
                        return 1;
                    }
                }
            }
        }
    }

    public static void game(ArrayList<Player> players,ArrayList<Card> fulldeck, Scanner scan){
        for (Player player : players) {
            ArrayList<Integer> indexes = new ArrayList<>();
            int n = 0;
            System.out.println("******************************************************************");
            System.out.println(player);
            System.out.println("ile kart chcesz wymienić(0 - 5). 0 oznacza żę nie chcesz wymienić kart");
            for (int i = 0; i < 2; i++) {
                try {
                    try {
                        n = Integer.parseInt(scan.next());
                        if (n > 5 || n < 0) {
                            throw new InputMismatchException();
                        }
                        break;
                    } catch (InputMismatchException e) {
                        System.out.println("podana wartość musi być liczbą z zakresu 0-5.\n masz jeszcze jedną próbe.");
                    }
                } catch (NumberFormatException e){
                    System.out.println("podana wartość musi być liczbą z zakresu 0-5.\n masz jeszcze jedną próbe.");
                }
            }
            System.out.println("wskaż karty do wymiany 1 - 5(od lewej)");
            for (int i = 0; i < 2; i++) {
                try {
                    try {
                        for (int j = 0; j < n; j++) {
                            int index = Integer.parseInt(scan.next());
                            if (index > 5 || index < 1) {
                                throw new InputMismatchException("podana wartość musi być liczbą z zakresu 1-5.\n masz jeszcze jedną próbe.");
                            } else if (indexes.contains(index)) {
                                throw new InputMismatchException("każda karte można wskazać tylko raz");
                            } else {
                                indexes.add(index);
                            }
                        }
                        break;
                    } catch (NumberFormatException e){
                        throw new InputMismatchException("podana wartość musi być liczbą z zakresu 1-5.\n masz jeszcze jedną próbe.");
                    }
                }
                catch (InputMismatchException e) {
                    System.out.println(e);
                }
            }
            Collections.sort(indexes);
            player.exchange(fulldeck, indexes);
            System.out.println(player);
        }
    }
    public static int rankingCompare(ArrayList<Player> players){
        int handRank  = players.get(0).handRanking();
        int p2HandRank  = players.get(1).handRanking();
        if (handRank > p2HandRank){
            return 0;
        } else if (handRank < p2HandRank){
            return 1;
        } else { // opis tych warunków w klasie player nad funkcją handRanking
            return handCompare(players,handRank);
        }
    }
    public static void showPlayerHands(Player player) {
        StringBuilder str;
        switch (player.handRanking()) {
            case 1:
                System.out.print(player.getNick() + ": Wysoka Karta ");
                str = new StringBuilder("[ ");
                for (Card card : player.gethand()) {
                    str.append(card).append(" ");
                }
                System.out.println(str + "]");
                break;
            case 2:
                System.out.print(player.getNick() + ": Para ");
                str = new StringBuilder("[ ");
                for (Card card : player.gethand()) {
                    str.append(card).append(" ");
                }
                System.out.println(str + "]");
                break;
            case 3:
                System.out.print(player.getNick() + ": Dwie Pary ");
                str = new StringBuilder("[ ");
                for (Card card : player.gethand()) {
                    str.append(card).append(" ");
                }
                System.out.println(str + "]");
                break;
            case 4:
                System.out.print(player.getNick() + ": Trójka ");
                str = new StringBuilder("[ ");
                for (Card card : player.gethand()) {
                    str.append(card).append(" ");
                }
                System.out.println(str + "]");
                break;
            case 5:
                System.out.print(player.getNick() + ": Strit ");
                str = new StringBuilder("[ ");
                for (Card card : player.gethand()) {
                    str.append(card).append(" ");
                }
                System.out.println(str + "]");
                break;
            case 6:
                System.out.print(player.getNick() + ": Kolor ");
                str = new StringBuilder("[ ");
                for (Card card : player.gethand()) {
                    str.append(card).append(" ");
                }
                System.out.println(str + "]");
                break;
            case 7:
                System.out.print(player.getNick() + ": Ful ");
                str = new StringBuilder("[ ");
                for (Card card : player.gethand()) {
                    str.append(card).append(" ");
                }
                System.out.println(str + "]");
                break;
            case 8:
                System.out.print(player.getNick() + ": Kareta ");
                str = new StringBuilder("[ ");
                for (Card card : player.gethand()) {
                    str.append(card).append(" ");
                }
                System.out.println(str + "]");
                break;
            case 9:
                System.out.print(player.getNick() + ": Poker ");
                str = new StringBuilder("[ ");
                for (Card card : player.gethand()) {
                    str.append(card).append(" ");
                }
                System.out.println(str + "]");
                break;
            case 10:
                System.out.print(player.getNick() + ": Poker Królewski ");
                str = new StringBuilder("[ ");
                for (Card card : player.gethand()) {
                    str.append(card).append(" ");
                }
                System.out.print(str + "]");
                break;

        }
    }

    public static ArrayList<Card> createDeck(){
        ArrayList<Card> fulldeck = new ArrayList<>();
        fulldeck.add(new Card("♣", "2", 2, 1));
        fulldeck.add(new Card("♣", "3", 3, 1));
        fulldeck.add(new Card("♣", "4", 4, 1));
        fulldeck.add(new Card("♣", "5", 5, 1));
        fulldeck.add(new Card("♣", "6", 6, 1));
        fulldeck.add(new Card("♣", "7", 7, 1));
        fulldeck.add(new Card("♣", "8", 8, 1));
        fulldeck.add(new Card("♣", "9", 9, 1));
        fulldeck.add(new Card("♣", "10", 10, 1));
        fulldeck.add(new Card("♣", "J", 11, 1));
        fulldeck.add(new Card("♣", "Q", 12, 1));
        fulldeck.add(new Card("♣", "K", 13, 1));
        fulldeck.add(new Card("♣", "A", 14, 1));

        fulldeck.add(new Card("♦", "2", 2, 2));
        fulldeck.add(new Card("♦", "3", 3, 2));
        fulldeck.add(new Card("♦", "4", 4, 2));
        fulldeck.add(new Card("♦", "5", 5, 2));
        fulldeck.add(new Card("♦", "6", 6, 2));
        fulldeck.add(new Card("♦", "7", 7, 2));
        fulldeck.add(new Card("♦", "8", 8, 2));
        fulldeck.add(new Card("♦", "9", 9, 2));
        fulldeck.add(new Card("♦", "10", 10, 2));
        fulldeck.add(new Card("♦", "J", 11, 2));
        fulldeck.add(new Card("♦", "Q", 12, 2));
        fulldeck.add(new Card("♦", "K", 13, 2));
        fulldeck.add(new Card("♦", "A", 14, 2));

        fulldeck.add(new Card("♥", "2", 2, 3));
        fulldeck.add(new Card("♥", "3", 3, 3));
        fulldeck.add(new Card("♥", "4", 4, 3));
        fulldeck.add(new Card("♥", "5", 5, 3));
        fulldeck.add(new Card("♥", "6", 6, 3));
        fulldeck.add(new Card("♥", "7", 7, 3));
        fulldeck.add(new Card("♥", "8", 8, 3));
        fulldeck.add(new Card("♥", "9", 9, 3));
        fulldeck.add(new Card("♥", "10", 10, 3));
        fulldeck.add(new Card("♥", "J", 11, 3));
        fulldeck.add(new Card("♥", "Q", 12, 3));
        fulldeck.add(new Card("♥", "K", 13, 3));
        fulldeck.add(new Card("♥", "A", 14, 3));

        fulldeck.add(new Card("♠", "2", 2, 4));
        fulldeck.add(new Card("♠", "3", 3, 4));
        fulldeck.add(new Card("♠", "4", 4, 4));
        fulldeck.add(new Card("♠", "5", 5, 4));
        fulldeck.add(new Card("♠", "6", 6, 4));
        fulldeck.add(new Card("♠", "7", 7, 4));
        fulldeck.add(new Card("♠", "8", 8, 4));
        fulldeck.add(new Card("♠", "9", 9, 4));
        fulldeck.add(new Card("♠", "10", 10, 4));
        fulldeck.add(new Card("♠", "J", 11, 4));
        fulldeck.add(new Card("♠", "Q", 12, 4));
        fulldeck.add(new Card("♠", "K", 13, 4));
        fulldeck.add(new Card("♠", "A", 14, 4));
        return fulldeck;
    }
    public static void main(String[] args) {
        /*Player test1 = new Player("test 1");
        test1.add(new Card("♠", "9", 9, 4));
        test1.add(new Card("♦", "2", 2, 2));
        test1.add(new Card("♥", "5", 5, 3));
        test1.add(new Card("♣", "6", 6, 1));
        test1.add(new Card("♣", "7", 7, 1));
        System.out.println(test1);

        Player test2 = new Player("test 2");
        test2.add(new Card("♣", "7", 7, 1));
        test2.add(new Card("♥", "9", 9, 3));
        test2.add(new Card("♦", "4", 4, 2));
        test2.add(new Card("♦", "3", 3, 2));
        test2.add(new Card("♠", "8", 8, 4));
        System.out.println(test2);

        ArrayList<Player> testArr = new ArrayList<>();
        testArr.add(test1);
        testArr.add(test2);
        System.out.println("wygrał gracz " + testArr.get(rankingCompare(testArr)).getNick());
        showPlayerHands(testArr.get(rankingCompare(testArr)));*/
        ArrayList<Card> fulldeck = createDeck();
        Collections.shuffle(fulldeck);

        ArrayList<Player> players = new ArrayList<>();
        players.add(new Player("gracz 1"));
        players.add(new Player("gracz 2"));
        for (int i = 0; i < 5; i++) {
            for (Player player : players) {
                player.deal(fulldeck);
            }
        }

        Scanner scan = new Scanner(System.in);
        game(players, fulldeck, scan);
        for (Player player : players) {
            System.out.print("wynik gracza ");
            showPlayerHands(player);
        }
        System.out.print("wygrał gracz ");
        showPlayerHands(players.get(rankingCompare(players)));
    }
}