package core;

import java.awt.BorderLayout;
import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;
import javax.swing.JLabel;
import javax.swing.JButton;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import javax.swing.JTextField;
import java.awt.Font;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

public class DB_Window extends JFrame {

	private JPanel contentPane;
	private JTextField year_t;
	private JTextField month_t;
	private JTextField day_t;
	private JTextField hour_t;
	private JTextField min_t;
	private JTextField name_t;

	/**
	 * Launch the application.
	 */
	public static void DBW() {
				try {
					DB_Window frame = new DB_Window();
					frame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
	}

	/**
	 * Create the frame.
	 */
	public DB_Window() {
		setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
		setBounds(100, 100, 450, 244);
		contentPane = new JPanel();
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		contentPane.setVisible(true);
		setContentPane(contentPane);
		contentPane.setLayout(null);
		
		JLabel operation = new JLabel("Adding or Removing Dates");
		operation.setVisible(true);
		operation.setFont(new Font("Yu Mincho Demibold", Font.PLAIN, 18));
		operation.setBounds(107, 11, 235, 30);
		contentPane.add(operation);
		
		JButton add = new JButton("Add");
		add.setVisible(true);
		add.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				
				DB_Connection.name = name_t.getText();DB_Connection.year = year_t.getText();DB_Connection.month = month_t.getText();DB_Connection.day = day_t.getText();DB_Connection.hour = hour_t.getText();DB_Connection.min = min_t.getText();
				DB_Connection.DB_ADD();
				
			}
			@Override
			public void mousePressed(MouseEvent e) {
				
				DB_Connection.name = name_t.getText();DB_Connection.year = year_t.getText();DB_Connection.month = month_t.getText();DB_Connection.day = day_t.getText();DB_Connection.hour = hour_t.getText();DB_Connection.min = min_t.getText();
				DB_Connection.DB_ADD();
				
			}
		});
		add.setBounds(335, 52, 89, 64);
		contentPane.add(add);
		
		JButton subtract = new JButton("Remove");
		subtract.setVisible(true);
		subtract.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				
				DB_Connection.name = name_t.getText();DB_Connection.year = year_t.getText();DB_Connection.month = month_t.getText();DB_Connection.day = day_t.getText();DB_Connection.hour = hour_t.getText();DB_Connection.min = min_t.getText();
				
			}
			@Override
			public void mousePressed(MouseEvent e) {
				
				DB_Connection.name = name_t.getText();DB_Connection.year = year_t.getText();DB_Connection.month = month_t.getText();DB_Connection.day = day_t.getText();DB_Connection.hour = hour_t.getText();DB_Connection.min = min_t.getText();
				DB_Connection.DB_REMOVE();
			}
		});
		subtract.setBounds(335, 127, 89, 64);
		contentPane.add(subtract);
		
		JLabel year_l = new JLabel("Year:");
		year_l.setVisible(true);
		year_l.setBounds(10, 77, 46, 14);
		contentPane.add(year_l);
		
		JLabel month_l = new JLabel("Month:");
		month_l.setVisible(true);
		month_l.setBounds(10, 102, 46, 14);
		contentPane.add(month_l);
		
		JLabel day_l = new JLabel("Day:");
		day_l.setVisible(true);
		day_l.setBounds(10, 127, 46, 14);
		contentPane.add(day_l);
		
		JLabel hour_l = new JLabel("Hour:");
		hour_l.setVisible(true);
		hour_l.setBounds(10, 152, 46, 14);
		contentPane.add(hour_l);
		
		JLabel minute_l = new JLabel("Minute:");
		minute_l.setVisible(true);
		minute_l.setBounds(10, 177, 46, 14);
		contentPane.add(minute_l);
		
		year_t = new JTextField();
		year_t.setVisible(true);
		year_t.setBounds(58, 74, 267, 20);
		contentPane.add(year_t);
		year_t.setColumns(10);
		
		month_t = new JTextField();
		month_t.setVisible(true);
		month_t.setColumns(10);
		month_t.setBounds(58, 99, 267, 20);
		contentPane.add(month_t);
		
		day_t = new JTextField();
		day_t.setVisible(true);
		day_t.setColumns(10);
		day_t.setBounds(58, 124, 267, 20);
		contentPane.add(day_t);
		
		hour_t = new JTextField();
		hour_t.setVisible(true);
		hour_t.setColumns(10);
		hour_t.setBounds(58, 149, 267, 20);
		contentPane.add(hour_t);
		
		min_t = new JTextField();
		min_t.setVisible(true);
		min_t.setColumns(10);
		min_t.setBounds(58, 174, 267, 20);
		contentPane.add(min_t);
		
		JLabel name_l = new JLabel("Name:");
		name_l.setVisible(true);
		name_l.setBounds(10, 52, 38, 14);
		contentPane.add(name_l);
		
		name_t = new JTextField();
		name_t.setVisible(true);
		name_t.setBounds(58, 49, 267, 20);
		contentPane.add(name_t);
		name_t.setColumns(10);
	}
}
