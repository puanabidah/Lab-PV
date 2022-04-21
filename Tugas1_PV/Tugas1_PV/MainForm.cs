/*
 * Created by SharpDevelop.
 * User: PUAN ABIDAH
 * Date: 21/04/2022
 * Time: 11:37
 * 
 * Nama Anggota : 
	- Faradhila Aulia Utami Tanjung (201401005)
	- Puan Abidah Nitisara (201401009)
 */
 
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
//tambahkan ini 
using System.Data;
using MySql.Data;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

namespace Tugas1_PV
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		MySqlConnection co = new MySqlConnection("Server = localhost; Database = data_produk; Uid = root");
		MySqlCommand mycommand = new MySqlCommand();
		MySqlDataAdapter myadapter = new MySqlDataAdapter();
		
		public void ReadData(){
			try{
				mycommand.Connection = co;
				myadapter.SelectCommand = mycommand;
				mycommand.CommandText = "select * from produk";
				DataSet ds = new DataSet();

				if (myadapter.Fill(ds,"tbl_barang") > 0){
					dataGridView1.DataSource = ds;
					dataGridView1.DataMember = "tbl_barang";
				}
			}
			catch (Exception ex){
				MessageBox.Show(ex.ToString());
			}
		} 
		
		public void InsertData(){
			// Memeriksa apakah kolom text kosong?
			if (textID.Text.Trim() == "" || textNama.Text.Trim() == "" || textHarga.Text.Trim() == "" || textWarna.Text.Trim() == "" || textKuantitas.Text.Trim() == "")
			{
				MessageBox.Show("Mohon isikan dahulu kolom-kolom yang tersedia");
			}
			else
			{
				/* insert data */
				try{
					mycommand.Connection=co;
					mycommand.CommandText="insert into produk values('"+textID.Text+"','"+textNama.Text+"','"+textHarga.Text+"','"+textWarna.Text+"','"+textKuantitas.Text+"')";
					myadapter.SelectCommand= mycommand;
					if (mycommand.ExecuteNonQuery()==1){
						MessageBox.Show("Data berhasil dimasukan","Informasi",MessageBoxButtons.OK,MessageBoxIcon.Information);
						ReadData();
						ResetData();
					}
				}
				catch(Exception ex){
					MessageBox.Show(ex.ToString());
				}
			}
		}
		
		public void UpdateData(){
			// Memeriksa apakah kolom text kosong?
			if (textID.Text.Trim() == "" || textNama.Text.Trim() == "" || textHarga.Text.Trim() == "" || textWarna.Text.Trim() == "" || textKuantitas.Text.Trim() == "")
			{
				MessageBox.Show("Mohon isikan dahulu kolom-kolom yang tersedia");
			}
			else
			{
				/* update data */
				try{
					mycommand.Connection=co;
					mycommand.CommandText = "update produk set id='"+textID.Text+"',nama='"+textNama.Text+"',harga='"+textHarga.Text+"',warna='"+textWarna.Text+"',kuantitas='"+textKuantitas.Text+"' where id ='"+textID.Text+"'";
					myadapter.SelectCommand= mycommand;
					if (mycommand.ExecuteNonQuery()==1){
						MessageBox.Show("Update data berhasil","Informasi",MessageBoxButtons.OK,MessageBoxIcon.Information);
						ReadData();
						ResetData();
					}
				}
				catch(Exception ex){
					MessageBox.Show(ex.ToString());
				}
			}
		}
		
		public void DeleteData(){
			// Memeriksa apakah kolom text kosong?
			if (textID.Text.Trim() == "" || textNama.Text.Trim() == "" || textHarga.Text.Trim() == "" || textWarna.Text.Trim() == "" || textKuantitas.Text.Trim() == "")
			{
				MessageBox.Show("Belum ada data yang terpilih");
			}
			
			else if (MessageBox.Show(textNama.Text + ", Yakin ingin dihapus?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				
				/* hapus data */
				try {
					mycommand.Connection=co;
					mycommand.CommandText="delete from produk where id='"+textID.Text+"'";
					myadapter.SelectCommand = mycommand;
					if (mycommand.ExecuteNonQuery()==1) {
						MessageBox.Show("Data berhasil dihapus");
						ReadData();
						ResetData();
					}
				}
				catch(Exception ex) {
					MessageBox.Show(ex.ToString());
				}
			}
		}
		
		public void ResetData(){
			textID.Text="";
			textNama.Text="";
			textHarga.Text="";
			textWarna.Text="";
			textKuantitas.Text="";
		}
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			co.Open();
			ReadData();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		
		void InsertBtnClick(object sender, EventArgs e)
		{
			InsertData();
			ResetData();			
		}
		
		
		void UpdateBtnClick(object sender, EventArgs e)
		{
			UpdateData();
			ResetData();
		}
		
		
		void DeleteBtnClick(object sender, EventArgs e)
		{
			DeleteData();	
			ResetData();
		}
		
		void ResetBtnClick(object sender, EventArgs e)
		{
			ResetData();
		}
		
		void DataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
		{
			textID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
			textNama.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
			textHarga.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
			textWarna.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
			textKuantitas.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
		}
	}
}
