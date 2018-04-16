package core;

import java.awt.BorderLayout;
import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;
import javax.swing.JLabel;
import java.awt.Font;
import javax.swing.JTextField;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;

public class GUI_DB extends JFrame {

	private JPanel contentPane;
	private JTextField url_f;
	private JTextField user_f;
	private JTextField pass_f;

	/**
	 * Launch the application.
	 */
	public static void DB_options() {
				try {
					GUI_DB frame = new GUI_DB();
					frame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}

	/**
	 * Create the frame.
	 */
	public GUI_DB() {
		addWindowListener(new WindowAdapter() {
			@Override
			public void windowClosed(WindowEvent e) {
				
				DB_Connection.URL = url_f.getText();
				DB_Connection.user = user_f.getText();
				DB_Connection.pass = pass_f.getText();
				
			}
		});
		setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
		setBounds(100, 100, 450, 161);
		contentPane = new JPanel();
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		contentPane.setVisible(true);
		setContentPane(contentPane);
		contentPane.setLayout(null);
		
		JLabel title = new JLabel("Database Options:");
		title.setVisible(true);
		title.setFont(new Font("Tahoma", Font.BOLD, 16));
		title.setBounds(138, 11, 155, 14);
		contentPane.add(title);
		
		JLabel url_l = new JLabel("URL:");
		url_l.setVisible(true);
		url_l.setBounds(23, 51, 29, 14);
		contentPane.add(url_l);
		
		url_f = new JTextField(DB_Connection.URL);
		url_f.setVisible(true);
		url_f.setBounds(49, 48, 375, 20);
		contentPane.add(url_f);
		url_f.setColumns(10);
		
		JLabel user_l = new JLabel("User Name:");
		user_l.setVisible(true);
		user_l.setBounds(10, 76, 82, 14);
		contentPane.add(user_l);
		
		user_f = new JTextField(DB_Connection.user);
		user_f.setVisible(true);
		user_f.setBounds(76, 73, 132, 20);
		contentPane.add(user_f);
		user_f.setColumns(10);
		
		JLabel pass_l = new JLabel("Password:");
		pass_l.setVisible(true);
		pass_l.setBounds(218, 79, 82, 14);
		contentPane.add(pass_l);
		
		pass_f = new JTextField(DB_Connection.pass);
		pass_f.setVisible(true);
		pass_f.setBounds(284, 73, 140, 20);
		contentPane.add(pass_f);
		pass_f.setColumns(10);
		
		JLabel connection = new JLabel("Connection: " + DB_Connection.status);
		connection.setVisible(true);
		connection.setBounds(171, 104, 122, 14);
		contentPane.add(connection);
	}
}
