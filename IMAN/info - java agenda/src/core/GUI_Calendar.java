package core;

import java.awt.BorderLayout;
import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;
import javax.swing.JButton;
import javax.swing.JLabel;
import java.awt.Font;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

public class GUI_Calendar extends JFrame {

	private JPanel contentPane;
	private JButton left;
	private JButton right;

	/**
	 * Launch the application.
	 */
	public static void calander() {
				try {
					GUI_Calendar frame = new GUI_Calendar();
					frame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}

	/**
	 * Create the frame.
	 */
	public GUI_Calendar() {
		setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
		setBounds(100, 100, 460, 330);
		contentPane = new JPanel();
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		contentPane.setVisible(true);
		setContentPane(contentPane);
		contentPane.setLayout(null);
		
		left = new JButton("<");
		left.setVisible(true);
		left.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				
				month_subtract();
				month_check();
				
			}
		});
		left.setBounds(107, 11, 40, 40);
		contentPane.add(left);
		
		right = new JButton(">");
		right.setVisible(true);
		right.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				
				month_add();
				
			}
		});
		right.setBounds(277, 11, 40, 40);
		contentPane.add(right);
		
		JLabel space = new JLabel("---------------------------------------------------------------------------------------------------------------");
		space.setVisible(true);
		space.setBounds(0, 53, 444, 14);
		contentPane.add(space);
		
		JButton b_31 = new JButton("31");
		b_31.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "31";
			}
		});
		b_31.setVisible(true);
		b_31.setBounds(359, 219, 45, 40);
		contentPane.add(b_31);
		
		JButton b_30 = new JButton("30");
		b_30.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "30";
			}
		});
		b_30.setVisible(true);
		b_30.setBounds(304, 219, 45, 40);
		contentPane.add(b_30);
		
		JButton b_29 = new JButton("29");
		b_29.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "29";
			}
		});
		b_29.setVisible(true);
		b_29.setBounds(254, 219, 45, 40);
		contentPane.add(b_29);
		
		JButton b_28 = new JButton("28");
		b_28.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "28";
			}
		});
		b_28.setVisible(true);
		b_28.setBounds(199, 219, 45, 40);
		contentPane.add(b_28);
		
		JButton b_27 = new JButton("27");
		b_27.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "27";
			}
		});
		b_27.setVisible(true);
		b_27.setBounds(144, 219, 45, 40);
		contentPane.add(b_27);
		
		JButton b_26 = new JButton("26");
		b_26.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "26";
			}
		});
		b_26.setVisible(true);
		b_26.setBounds(89, 219, 45, 40);
		contentPane.add(b_26);
		
		JButton b_25 = new JButton("25");
		b_25.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "25";
			}
		});
		b_25.setVisible(true);
		b_25.setBounds(37, 219, 45, 40);
		contentPane.add(b_25);
		
		JButton b_24 = new JButton("24");
		b_24.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "24";
			}
		});
		b_24.setVisible(true);
		b_24.setBounds(389, 168, 45, 40);
		contentPane.add(b_24);
		
		JButton b_23 = new JButton("23");
		b_23.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "23";
			}
		});
		b_23.setVisible(true);
		b_23.setBounds(334, 168, 45, 40);
		contentPane.add(b_23);
		
		JButton b_22 = new JButton("22");
		b_22.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "22";
			}
		});
		b_22.setVisible(true);
		b_22.setBounds(279, 167, 45, 43);
		contentPane.add(b_22);
		
		JButton b_21 = new JButton("21");
		b_21.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "21";
			}
		});
		b_21.setVisible(true);
		b_21.setBounds(224, 168, 45, 40);
		contentPane.add(b_21);
		
		JButton b_20 = new JButton("20");
		b_20.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "20";
			}
		});
		b_20.setVisible(true);
		b_20.setBounds(169, 168, 45, 40);
		contentPane.add(b_20);
		
		JButton b_19 = new JButton("19");
		b_19.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "19";
			}
		});
		b_19.setVisible(true);
		b_19.setBounds(114, 168, 45, 40);
		contentPane.add(b_19);
		
		JButton b_18 = new JButton("18");
		b_18.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "18";
			}
		});
		b_18.setVisible(true);
		b_18.setBounds(62, 168, 45, 40);
		contentPane.add(b_18);
		
		JButton b_17 = new JButton("17");
		b_17.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "17";
			}
		});
		b_17.setVisible(true);
		b_17.setBounds(10, 168, 45, 40);
		contentPane.add(b_17);
		
		JButton b_16 = new JButton("16");
		b_16.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "16";
			}
		});
		b_16.setVisible(true);
		b_16.setBounds(389, 117, 45, 40);
		contentPane.add(b_16);
		
		JButton b_15 = new JButton("15");
		b_15.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "15";
			}
		});
		b_15.setVisible(true);
		b_15.setBounds(334, 117, 45, 40);
		contentPane.add(b_15);
		
		JButton b_14 = new JButton("14");
		b_14.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "14";
			}
		});
		b_14.setVisible(true);
		b_14.setBounds(277, 117, 45, 40);
		contentPane.add(b_14);
		
		JButton b_13 = new JButton("13");
		b_13.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "13";
			}
		});
		b_13.setVisible(true);
		b_13.setBounds(224, 117, 45, 40);
		contentPane.add(b_13);
		
		JButton b_12 = new JButton("12");
		b_12.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "12";
			}
		});
		b_12.setVisible(true);
		b_12.setBounds(169, 114, 45, 43);
		contentPane.add(b_12);
		
		JButton b_11 = new JButton("11");
		b_11.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "11";
			}
		});
		b_11.setVisible(true);
		b_11.setBounds(114, 116, 45, 43);
		contentPane.add(b_11);
		
		JButton b_10 = new JButton("10");
		b_10.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "10";
			}
		});
		b_10.setVisible(true);
		b_10.setBounds(62, 114, 45, 43);
		contentPane.add(b_10);
		
		JButton b_9 = new JButton("9");
		b_9.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "9";
			}
		});
		b_9.setVisible(true);
		b_9.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
			}
		});
		b_9.setBounds(10, 115, 45, 40);
		contentPane.add(b_9);
		
		JButton b_8 = new JButton("8");
		b_8.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "8";
			}
		});
		b_8.setVisible(true);
		b_8.setBounds(389, 66, 45, 40);
		contentPane.add(b_8);
		
		JButton b_7 = new JButton("7");
		b_7.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "7";
			}
		});
		b_7.setVisible(true);
		b_7.setBounds(334, 66, 45, 40);
		contentPane.add(b_7);
		
		JButton b_6 = new JButton("6");
		b_6.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "6";
			}
		});
		b_6.setVisible(true);
		b_6.setBounds(277, 66, 45, 40);
		contentPane.add(b_6);
		
		JButton b_5 = new JButton("5");
		b_5.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "5";
			}
		});
		b_5.setVisible(true);
		b_5.setBounds(224, 66, 45, 40);
		contentPane.add(b_5);
		
		JButton b_4 = new JButton("4");
		b_4.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "4";
			}
		});
		b_4.setVisible(true);
		b_4.setBounds(169, 66, 45, 40);
		contentPane.add(b_4);
		
		JButton b_3 = new JButton("3");
		b_3.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "3";
			}
		});
		b_3.setVisible(true);
		b_3.setBounds(117, 65, 45, 40);
		contentPane.add(b_3);
		
		JButton b_2 = new JButton("2");
		b_2.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "2";
			}
		});
		b_2.setVisible(true);
		b_2.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
			}
		});
		b_2.setBounds(62, 66, 45, 40);
		contentPane.add(b_2);
		
		JButton b_1 = new JButton("1");
		b_1.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {DB_Connection.day = "1";
			}
		});
		b_1.setVisible(true);
		b_1.setBounds(10, 66, 45, 40);
		contentPane.add(b_1);
		
		JLabel date = new JLabel("0/0/0");
		date.setVisible(true);
		date.setFont(new Font("Tahoma", Font.BOLD, 14));
		date.setBounds(194, 22, 62, 14);
		contentPane.add(date);
		
		month_check();
		
	}
	
	public static void month_check(){	System.out.println("Checked the Month!!! It's the " + DB_Connection.month); if(DB_Connection.month.equals(1)){}}
	public static void month_add(){		System.out.println("+1"); }
	public static void month_subtract(){		System.out.println("-1"); }
	
}
