import java.util.Scanner;

public class Test {
    public static void main(String[] args) {
        MyData.info();
        System.out.println("Witaj w świecie javy " + String.valueOf(Character.toChars(0x1F603)));

        String name = "Józef";
        Scanner sc = new Scanner(System.in);
        System.out.print("Podaj imię: ");
        String name2 = sc.next();
        if(name.equals(name2))
            System.out.println("Imiona są takie same");
        else
            System.out.println("Imiona się różnią");

    }
}
