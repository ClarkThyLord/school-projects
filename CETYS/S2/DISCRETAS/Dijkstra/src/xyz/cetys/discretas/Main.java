package xyz.cetys.discretas;

import java.util.Scanner;
import java.io.BufferedReader;
import java.io.InputStreamReader;

public class Main {
    public static void main(String[] args) {
        Scanner in = new Scanner(new BufferedReader(new InputStreamReader()));

        // Scanner has functions to read ints, longs, strings, chars, etc.
        int v = in.nextInt();
        int a = in.nextInt();
        int z = in.nextInt();

        int[][] matrix = new int[v][v];

        for (int i = 0; i < v; i++) {
            for (int j = 0; j < v; j++){
                matrix[i][j] = in.nextInt();
                //System.out.println("i= " +i + " j= "+j+" valor = "+matrix[i][j]);
            }
        }

        String out1 = dijkstra(matrix, a, z, v);
        System.out.println("Dijkstra: "+ out1);
    }

    private static String dijkstra(int[][] matrix, int v, int a, int z) {
        int[][] vertices = new int[4][v];

        for (int current = a; current < v; current++)
        {
            if (vertices[3][current] == 1) continue;
            else vertices[3][current] = 1;

            for (int sub_current = 0; sub_current < v; sub_current++) {
                int cost = matrix[current][sub_current];

                if (cost != 0 && vertices[3][sub_current] == 0) vertices[3][sub_current] = 1;
            }
        }

        return "";
    }
}
