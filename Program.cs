using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GetPixel
{
	using System.Net;
	using System.Runtime.InteropServices.WindowsRuntime;
	using System.Xml.Schema;

	class Program
	{
		private const int dim = 10;
		private const double fillPoint = 0.3;
		static void Main( string[] args )
		{

				Bitmap bmp = null;
			//The Source Directory in debug\bin\Big\
			//	string[] files = Directory.GetFiles( "Big\\" );
			//	foreach ( string filename in files )
			//	{
			//		bmp = ( Bitmap )Image.FromFile( filename );
			//		bmp = ChangeColor( bmp );
			//		string[] spliter = filename.Split( '\\' );
			//		//Destination Directory debug\bin\BigGreen\
			//		bmp.Save( "BigGreen\\" + spliter[ 1 ] );
			//	}
			//}
			//catch ( System.Exception ex )
			//{
			//	Console.WriteLine( ex.ToString() );
			//}


			//bmp = new Bitmap( Image.FromFile( "jpg\\sample1.jpg" ) );
			bmp = new Bitmap( Image.FromFile( $"jpg\\sample0.jpg" ) );

			for ( int i = 1; i < 10; i++ )
			{
				bmp = ChangeColor( bmp );
			}
			bmp.Save( $"jpg\\sample-new.jpg", System.Drawing.Imaging.ImageFormat.Jpeg );



		}

		public static Boolean ValidateArea( Bitmap scrBitmap, int x , int y, int num)
		{
			bool flag = true;
		
			int tbl_size_x = dim;
			int tbl_size_y = dim;
		


			int tb_x = 0;
			int tb_y = 0;
			int tblValue = 0;
			int tblSumm = dim*dim;

			int tbl_x_start = x - tbl_size_x / 2;
			int tbl_y_start = y - tbl_size_y / 2;
			int tbl_x_end = x + tbl_size_x / 2;
			int tbl_y_end = y + tbl_size_y / 2;



			//if ( ( x + tbl_size_x ) > scrBitmap.Size.Width )
			//{
			//	tbl_size_x = tbl_size_x - (x + tbl_size_x  - scrBitmap.Size.Width);
			//	tbl_x_end = scrBitmap.Size.Width;
			//}

			//if ( ( y + tbl_size_y ) > scrBitmap.Size.Height )
			//{
			//	tbl_size_y = tbl_size_y - ( x + tbl_size_y - scrBitmap.Size.Height );
			//	tbl_y_end = scrBitmap.Size.Height;
			//}


			//if ( x <= tbl_size_x )
			//{
			//	tbl_size_x = tbl_size_x - x;
			//	tbl_x_start = 0;
			//	tbl_x_end = tbl_size_x - ( tbl_size_x - x) ;
			//}

			//if ( y <= tbl_size_y )
			//{
			//	tbl_size_y = tbl_size_y - y;
			//	tbl_y_start = 0;
			//	tbl_y_end = tbl_size_y - ( tbl_size_y- y );
			//}

			var table = new int?[ tbl_size_y, tbl_size_x ];
			Bitmap table_step = new Bitmap( tbl_size_x, tbl_size_y );


			for ( int i = tbl_x_start; i < tbl_x_end; i++ )
			{
				tb_y = 0;
				for ( int j = tbl_y_start; j < tbl_y_end; j++ )
				{
					Color actualColor = scrBitmap.GetPixel( i, j );
					table_step.SetPixel( tb_x, tb_y, actualColor );
					if ( actualColor.Name != "ffffffff" )
					{
						table[ tb_y, tb_x ] = 1;
						tblValue = tblValue + 1;
						
					}
					else
					{
						int XX = x;
						int YY = y;

						table[ tb_x, tb_y ] = 0;
					}
					tb_y++;

				}
				tb_x++;
			}

			//if( ( x < 30 && y < 90 ) && ( x > 20 && y > 60 ) )
			if(  x == 39 && y == 75 )
			{
				table_step.Save( $"jpg\\steps\\sample1-{num}.jpg", System.Drawing.Imaging.ImageFormat.Jpeg );
				Console.WriteLine( tblValue );
				Console.WriteLine( ( ( double )tblValue / ( double )tblSumm ) );

			}


			if ( (( double )tblValue / ( double )tblSumm) > fillPoint )
			{
				flag = false;
			}


			return flag;
		}

		public static void SaveBitmap( Bitmap bmp, int num)
		{
				bmp.Save( @"jpg\\steps\\sample1-{num}.jpg", System.Drawing.Imaging.ImageFormat.Jpeg );
		}


		public static Bitmap ChangeColor( Bitmap scrBitmap )
		{
			//You can change your new color here. Red,Green,LawnGreen any..
			Color newColor = Color.White;
			Color actualColor;
			int step = 1; 
			//make an empty bitmap the same size as scrBitmap
			Bitmap newBitmap = new Bitmap( scrBitmap.Width, scrBitmap.Height );
			for ( int i = dim; i < scrBitmap.Width-dim; i++ )
			{
				for ( int j = dim; j < scrBitmap.Height-dim; j++ )
				{
					//get the pixel from the scrBitmap image
					actualColor = scrBitmap.GetPixel( i, j );
					// > 150 because.. Images edges can be of low pixel colr. if we set all pixel color to new then there will be no smoothness left.
					if (ValidateArea(scrBitmap, i, j, step) )
						newBitmap.SetPixel( i, j, newColor );
					else
						newBitmap.SetPixel( i, j, actualColor );
						step++;
				}
			}
			return newBitmap;
		}
	}
}