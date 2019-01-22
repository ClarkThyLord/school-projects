package core;

import java.awt.BorderLayout;
import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;
import javax.swing.JMenuBar;
import javax.swing.JMenu;
import javax.swing.JMenuItem;
import javax.swing.JSeparator;
import javax.swing.JLabel;
import java.awt.Font;
import javax.swing.JTextField;
import javax.swing.JButton;
import javax.swing.SwingConstants;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import javax.swing.JTextArea;

public class GUI_Startup extends JFrame {

	private JPanel parent;
	private JTextField month_f;
	private JTextField day_f;
	private JTextField year_f;
	private JTextArea txtrNothingToDisplay;

	/**
	 * Launch the application.
	 */
	public static void startup() {
				try {
					GUI_Startup frame = new GUI_Startup();
					frame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}

	/**
	 * Create the frame.
	 */
	public GUI_Startup() {
		setTitle("Java Agenda");
		setResizable(false);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setBounds(100, 100, 800, 600);
		
		JMenuBar bar = new JMenuBar();
		bar.setVisible(true);
		setJMenuBar(bar);
		
		JMenu options = new JMenu("Options");
		options.setVisible(true);
		bar.add(options);
		
		JMenuItem refresh_ti = new JMenuItem("Refresh");
		refresh_ti.setVisible(true);
		refresh_ti.addMouseListener(new MouseAdapter() {
			@Override
			public void mousePressed(MouseEvent e) {
				
				DB_Connection.DB_connect();
				
			}
		});
		options.add(refresh_ti);
		
		JMenuItem database_ti = new JMenuItem("Database");
		database_ti.setVisible(true);
		database_ti.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
			
				GUI_DB.DB_options();
			
			}
			@Override
			public void mousePressed(MouseEvent e) {
				
				GUI_DB.DB_options();
				
			}
		});
		options.add(database_ti);
		
		JSeparator separator = new JSeparator();
		separator.setVisible(true);
		options.add(separator);
		
		JMenuItem exit_ti = new JMenuItem("Exit");
		exit_ti.setVisible(false);
		options.add(exit_ti);
		
		JMenu windows = new JMenu("Windows");
		windows.setVisible(true);
		bar.add(windows);
		
		JMenuItem calendar_ti = new JMenuItem("Calendar");
		calendar_ti.setVisible(true);
		calendar_ti.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				
				GUI_Calendar.calander();
				
			}
			@Override
			public void mousePressed(MouseEvent e) {
				
				GUI_Calendar.calander();
				
			}
		});
		windows.add(calendar_ti);
		
		JMenuItem datemod_ti = new JMenuItem("Add/Remove Dates");
		datemod_ti.setVisible(true);
		datemod_ti.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				
				DB_Window.DBW();
				
			}
			@Override
			public void mousePressed(MouseEvent e) {
				
				DB_Window.DBW();
				
			}
		});
		windows.add(datemod_ti);
		parent = new JPanel();
		parent.setBorder(new EmptyBorder(5, 5, 5, 5));
		parent.setVisible(true);
		setContentPane(parent);
		parent.setLayout(null);
		
		JPanel secound = new JPanel();
		secound.setBounds(0, 0, 794, 550);
		parent.add(secound);
		secound.setLayout(null);
		
		JLabel title = new JLabel("Search for Date:");
		title.setVisible(true);
		title.setFont(new Font("Tahoma", Font.BOLD | Font.ITALIC, 17));
		title.setBounds(180, 11, 158, 16);
		secound.add(title);
		
		JLabel month_l = new JLabel("Month ~");
		month_l.setVisible(true);
		month_l.setFont(new Font("Tahoma", Font.BOLD, 13));
		month_l.setBounds(24, 40, 70, 14);
		secound.add(month_l);
		
		month_f = new JTextField();
		month_f.setVisible(true);
		month_f.setBounds(84, 38, 86, 20);
		secound.add(month_f);
		month_f.setColumns(10);
		
		JLabel day_l = new JLabel("Day ~");
		day_l.setVisible(true);
		day_l.setFont(new Font("Tahoma", Font.BOLD, 13));
		day_l.setBounds(180, 40, 46, 14);
		secound.add(day_l);
		
		day_f = new JTextField();
		day_f.setVisible(true);
		day_f.setBounds(225, 38, 86, 20);
		secound.add(day_f);
		day_f.setColumns(10);
		
		JLabel year_l = new JLabel("Year ~");
		year_l.setVisible(true);
		year_l.setFont(new Font("Tahoma", Font.BOLD, 13));
		year_l.setBounds(321, 41, 55, 14);
		secound.add(year_l);
		
		year_f = new JTextField();
		year_f.setVisible(true);
		year_f.setBounds(374, 38, 86, 20);
		secound.add(year_f);
		year_f.setColumns(10);
		
		JButton enter_b = new JButton("Enter");
		enter_b.setVisible(true);
		enter_b.setBounds(470, 11, 135, 23);
		secound.add(enter_b);
		
		JButton clear_b = new JButton("Clear");
		clear_b.setVisible(true);
		clear_b.setBounds(470, 41, 135, 23);
		secound.add(clear_b);
		
		JButton datemod_b = new JButton("Add/Remove Dates");
		datemod_b.setVisible(true);
		datemod_b.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				
				DB_Window.DBW();
				
			}
		});
		datemod_b.setBounds(615, 11, 169, 53);
		secound.add(datemod_b);
		
		JLabel space = new JLabel("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
		space.setVisible(true);
		space.setBounds(0, 63, 794, 14);
		secound.add(space);
		
		JLabel l_1 = new JLabel("Date Being Shown:");
		l_1.setVisible(true);
		l_1.setFont(new Font("Tahoma", Font.BOLD | Font.ITALIC, 13));
		l_1.setBounds(10, 82, 129, 20);
		secound.add(l_1);
		
		JLabel date = new JLabel("0/0/0");
		date.setVisible(true);
		date.setFont(new Font("Tahoma", Font.BOLD, 12));
		date.setBounds(140, 86, 135, 14);
		secound.add(date);
		
		txtrNothingToDisplay = new JTextArea();
		txtrNothingToDisplay.setVisible(true);
		txtrNothingToDisplay.setFont(new Font("Segoe UI Semilight", Font.BOLD | Font.ITALIC, 14));
		txtrNothingToDisplay.setText("Nothing To Display....");
		txtrNothingToDisplay.setBounds(10, 113, 774, 426);
		secound.add(txtrNothingToDisplay);
	}
}
