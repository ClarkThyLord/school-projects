import java.lang.*;
import java.math.*;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.logging.ConsoleHandler;

public class Main {
    public static void main(String[] args) {
        mcd(36, 24);
        mod(143, 89, 187);
        euler(2018);
        factor(24);
    }

    private static BigInteger mcd(int a, int b){
        BigInteger one = BigInteger.valueOf(1), max = BigInteger.valueOf(a).gcd(BigInteger.valueOf(b));

        if (max.equals(one)) System.out.println(a + " y " + b + " son primos relativos");
        else System.out.println("El maximo comun divisor de " + a + " y " + b + " es " + max);

        return max;
    }

    private static BigInteger mod(int n, int r, int m){
        BigInteger mod = BigInteger.valueOf(n).modPow(BigInteger.valueOf(r), BigInteger.valueOf(m));

        System.out.println(n + "^" + r + " congruente a " + mod + " mod " + m);

        return mod;
    }

    private static void euler(int n){
        for (int p=2; p < n/2; p++) {
            if (n % p == 0){
                int q = n / p;
                int phi = (p - 1) * (q - 1);
                System.out.println("N = " + p + " * " + q);
                System.out.println("Phi = " + phi);
                return;
            }
        }

        System.out.println("No se encontro phi...");
    }

    private static String factor(int n){
        String result = "";

        List<Integer> list = new ArrayList<Integer>();

        int temp = n;
        while (true) {
            for (int i = 2; i < n; i++) {
                if (temp % i == 0) {
                    list.add(i);
                    temp = temp / i;
                    break;
                }
            }
            if (temp == 1) break;
        }

        System.out.println(list.toString());

        int pos = 0, last = list.get(0), count = 1;
        for (int  i = 1; i < list.size(); i++){
            if (last == list.get(i)) {
                last = list.get(i);
                count += 1;
            } else {
                System.out.print((pos > 0 ? " + " : "") + last + (count > 0 ? "^" + count : ""));
                pos++;
                count = 0;
            }
        }

        HashMap<Integer, Integer> sums = new HashMap<Integer, Integer>();

        for (int i = 0; i < list.size(); i++) sums.put(list.get(i), (sums.containsKey(list.get(i)) ? sums.get(i) + 1 : 0));

        System.out.println(sums);

        return result;
    }
}
