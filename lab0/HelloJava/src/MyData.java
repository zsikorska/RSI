import java.net.InetAddress;
import java.net.UnknownHostException;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.Locale;

public class MyData {

    public static void info() {
        System.out.println("stud1, index1");
        System.out.println("stud2, index2");

        System.out.println(LocalDateTime.now().format(DateTimeFormatter.ofPattern("dd MMMM HH:mm:ss",
                Locale.forLanguageTag("pl-PL"))));


        System.out.println(System.getProperty("java.version"));
        System.out.println(System.getProperty("user.name"));
        System.out.println(System.getProperty("os.name"));
        try {
            System.out.println(InetAddress.getLocalHost().getHostAddress());
        }
        catch (UnknownHostException e){
            System.err.print(e);
        }





    }



}
