using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.Util;
using Emgu.CV.UI;
using Emgu.CV.Structure;

namespace HW2_102378056
{
    public partial class Form1Main : Form
    {
        //宣告 
        bool bM1=false, bM2=false, bM3=false;       //確認矩陣目前是否存在內容之布林值  預設false 若矩陣放入值則會變成true
        Matrix<float> M1, M2, M3,M4;                   //宣告三個矩陣 M1 M2 M3 M1和M2是放隨機產生的矩陣值  M3是放處理過後的矩陣值(轉置 反矩陣等等)
        public Form1Main()
        {
            InitializeComponent();
        }

/******************************************************************************/
/*                                 產生新矩陣                                 */
/******************************************************************************/

        private void NewMatrix_Click(object sender, EventArgs e)
        {
            if ((int)Num_Matrix.Value == 1)                                             //讀取選單的值 判斷要產生一個或是兩個新矩陣,這邊是產生一個
            {
                Cleanbox();
                M1 = new Matrix<float>((int)Mat_Row.Value, (int)Mat_Col.Value);         //讀取選單的值 產生row*col大小的M1矩陣
                bM1 = true;                                                             //設定M1矩陣存在的布林值
                randomMatrix(M1);                                                       //隨機設定矩陣內元素的值
                PrintMatrix(M1, Matrix1);                                               //在名為Matrix1的richbox中印出矩陣M1
            }
            if ((int)Num_Matrix.Value == 2)                                             //產生兩個新矩陣
            {
                Cleanbox();
                M1 = new Matrix<float>((int)Mat_Row.Value, (int)Mat_Col.Value);
                M2 = new Matrix<float>((int)Mat_Row.Value, (int)Mat_Col.Value);
                bM1 = true;                                                             //設定M1矩陣存在的布林值
                bM2 = true;                                                             //設定M2矩陣存在的布林值
                randomMatrix(M1);
                randomMatrix(M2);
                PrintMatrix(M1, Matrix1);
                PrintMatrix(M2, Matrix2);                                               //在名為Matrix2的richbox中印出矩陣M2
            }
        }
        /******************************************************************************/
        /*                                 印出矩陣                                   */
        /******************************************************************************/
        private void PrintMatrix(Matrix<float> Mat, RichTextBox richbox)
        {

            for (int i = 0; i < Mat.Rows; i++)
            {
                string s = "";
                for (int j = 0; j < Mat.Cols; j++)
                {
                    s = s + String.Format("{0:0.000000000}",Mat[i, j])+"  \t";          //設定印出矩陣的格式
                }
                s = s + "\n";
                richbox.AppendText(s);
            }
        }
        /******************************************************************************/
        /*                                 隨機產生矩陣內的值                         */
        /******************************************************************************/
        private void randomMatrix(Matrix<float> Mat)
        {
            Random crandom = new Random(Guid.NewGuid().GetHashCode());  //利用 Guid.NewGuid()每一次所產生出來的結果都是不同的，再利用它產生雜湊碼來當成亂數產生器的種子，產生出真的亂數。
            for (int i = 0; i < Mat.Rows; i++)
            {
                for (int j = 0; j < Mat.Cols; j++)
                {
                    Mat[i, j] = (float)crandom.NextDouble();
                }
            }
        }
        /******************************************************************************/
        /*                                 清除按鈕                                   */
        /******************************************************************************/
        private void Clean_Click(object sender, EventArgs e)
        {
            Cleanbox();                                                         //呼叫清除函式
        }
        /******************************************************************************/
        /*                    清除顯示文字矩陣 釋放矩陣資源                           */
        /******************************************************************************/
        private void Cleanbox()
        {
            if (M1 != null)
            { 
                M1.Dispose();               //釋放M1的資源
                bM1 = false;                //矩陣M1存在的布林值改為false
            }          
            if (M2 != null) 
            {
                M2.Dispose();               //釋放M2的資源 
                bM2 = false;                //矩陣M2存在的布林值改為false 
            }
            if (M3 != null) 
            {
                M3.Dispose();               //釋放M3的資源 
                bM3 = false;                //矩陣M3存在的布林值改為false 
            }
            Matrix1.Text = "";              //清空畫面上的richbox
            Matrix2.Text = "";
            Result.Text = "";
        }
        /******************************************************************************/
        /*                                 取得矩陣元素                               */
        /******************************************************************************/
        private void GetElement_Click(object sender, EventArgs e)
        {

           if (bM1==true && (int)GE_No.Value == 1)                                                                  //判斷矩陣是否存在 且判斷所要取出元素值的矩陣編號
            {
               if ((int)GE_R.Value <= (int)M1.Rows && (int)GE_C.Value <= (int)M1.Cols)                              //檢查所要取出的元素位置是否存在於矩陣內
               {
                   float ele = M1[(int)GE_R.Value - 1, (int)GE_C.Value - 1];                                        //取出矩陣值
                   Result.Text = "The element Matrix1[" + GE_R.Value + "," + GE_C.Value + "] is  " + ele + "\n";    //印出結果
               }
               else
               {
                   Result.Text = "The element Matrix1[" + GE_R.Value + "," + GE_C.Value + "] is is empty!!\n";      //檢查所要取出的元素位置不存在於矩陣內 印岀警告
               }   
            }
            if(bM2==true && (int)GE_No.Value == 2)
            {

                if ((int)GE_R.Value <= (int)M2.Rows && (int)GE_C.Value <= (int)M2.Cols)
                {
                    float ele = M2[(int)GE_R.Value - 1, (int)GE_C.Value - 1];
                    Result.Text = "The element Matrix2[" + GE_R.Value + "," + GE_C.Value + "] is  " + ele + "\n";
                }
                else
                {
                    Result.Text = "The element Matrix2[" + GE_R.Value + "," + GE_C.Value + "] is empty!!\n";
                }   
            }
            if (((int)GE_No.Value == 1 && bM1 == false) || ((int)GE_No.Value == 2 && bM2 == false))      //檢查所要取出元素的矩陣不存在 印岀警告
            {
                Result.Text = "Matrix " + GE_No.Value + " is empty!!\n";
            }
        }
        /******************************************************************************/
        /*                                 取得矩陣整列值                             */
        /******************************************************************************/
        private void GetRow_Click(object sender, EventArgs e)
        {
            Result.Text = "";
            if ((int)GR_No.Value == 1 && bM1 == true)                                                            //判斷矩陣是否存在 且判斷所要取出列的矩陣編號
            {
                if ((int)GR_R.Value <= (int)M1.Rows)                                                             //檢查所要取出的列是否存在於矩陣內
                {
                    Result.Text = "The row" + GR_R.Value + " of Matrax1 is\n";
                    PrintMatrix(M1.GetRow((int)GR_R.Value - 1), Result);                                        //用GetRow()取出矩陣列 並印岀
                }
                else
                {
                    Result.Text = "The row of Matrix1 is empty!!\n";                                            //檢查所要取出的列不存在於矩陣內 印岀警告
                }
            }
            if ((int)GR_No.Value == 2 && bM2 == true)
            {
                if ((int)GR_R.Value <= (int)M2.Rows)
                {
                    Result.Text = "The row" + GR_R.Value + " of Matrax2 is\n";
                    PrintMatrix(M2.GetRow((int)GR_R.Value - 1),Result);
                }
                else
                {
                    Result.Text = "The row of Matrix2 is empty!!\n";
                }
            }
            if (((int)GR_No.Value == 1 && bM1 == false) || ((int)GR_No.Value == 2 && bM2 == false))             //檢查所要取出列的矩陣不存在 印岀警告
            {
                Result.Text = "Matrix " + GR_No.Value + " is empty!!\n";
            }
        }
        /******************************************************************************/
        /*                                 轉置矩陣                                   */
        /******************************************************************************/
        private void Transpose_Click(object sender, EventArgs e)
        {
            if (bM3 == true)                                                //若有舊的M3矩陣資源未釋放 先釋放資源以防錯誤
            {
                M3.Dispose();
                bM3 = false;
            }

            if (bM1 == true)                                                //確定矩陣M1矩陣存在 以後才轉置M1矩陣   
            {
                M3 = new Matrix<float>((int)M1.Cols, (int)M1.Rows);         //宣告M3矩陣大小為M1的col*row 放置轉置後的M1
                bM3 = true;                                                 //M3 矩陣存在 所以布林值改為true
                CvInvoke.cvTranspose(M1, M3);                               //轉置M1矩陣 放到M3
                Result.Text = Result.Text+"Matrix 1's Transpose:\n";
                PrintMatrix(M3,Result);                                     //印出M3
            }
            if (bM2 == true)                                               //確定矩陣M2矩陣存在 以後才轉置M2矩陣  
            {
                M3 = new Matrix<float>((int)M2.Cols, (int)M2.Rows);
                bM3 = true;
                CvInvoke.cvTranspose(M2, M3);
                Result.Text =Result.Text+ "Matrix 2's Transpose:\n";
                PrintMatrix(M3, Result);
            }
            if (bM1 == false)                                               //矩陣不存在時 不轉置
            {
                Result.Text = Result.Text + "Matrix 1 is empty!!\n";
            }
            if (bM2 == false)
            {
                Result.Text = Result.Text + "Matrix 2 is empty!!\n";
            }
        }
        /******************************************************************************/
        /*                                 矩陣相乘                                   */
        /******************************************************************************/
        private void Mat_Mult_Click(object sender, EventArgs e)
        {
            Result.Text = "";
            if (bM3 == true)                                                //若有舊的M3矩陣資源未釋放 先釋放資源以防錯誤
            {
                M3.Dispose();
                bM3 = false;
            }
            if (bM1 == false)                                              //矩陣有任一不存在時 不相乘 
            {
                Result.Text = Result.Text + "Matrix 1 is empty!!\n";
            }
            else if (bM2 == false)
            {
                Result.Text = Result.Text + "Matrix 2 is empty!!\n";
            }
            else
            {
                M3 = new Matrix<float>((int)M1.Rows, (int)M2.Cols);         //宣告M3矩陣大小為M1的row*M2的col  放置相乘後的M1*M2
                bM3 = true;                                                 //M3 矩陣存在 所以布林值改為true
                CvInvoke.cvMul(M1, M2, M3, 1);                              //兩矩陣相乘後放到M3
                Result.Text = Result.Text + "Matrix 1 x Matrix2:\n";
                PrintMatrix(M3, Result);                                    //印出結果
            }

        }
        /******************************************************************************/
        /*                                 計算行列式                                 */
        /******************************************************************************/
        private void Determinant_Click(object sender, EventArgs e)
        {
            double det=0;
            if (bM1 == true)                                        //確認矩陣存在才計算行列式
            {
                if ((int)M1.Cols == (int)M1.Rows)                   //確認矩陣row=col 要正方形矩陣才能計算行列式
                {
                    det=CvInvoke.cvDet(M1);                         //計算M1矩陣的行列式
                    Result.Text = Result.Text + "Matrix 1's Determinant:  "+det+"\n";
                }
                else {
                    Result.Text = Result.Text + "Matrix1 is a Non-square matrix.\nNon-square matrices (m-by-n matrices for which m ≠ n) do not have a determinant.\n";
                }
            }
            if (bM2 == true)
            {
                if ((int)M2.Cols == (int)M2.Rows)
                {
                    det=CvInvoke.cvDet(M2);
                    Result.Text = Result.Text + "Matrix 2's Determinant:  " + det + "\n";
                }
                else
                {
                    Result.Text = Result.Text + "Matrix2 is a Non-square matrix.\nNon-square matrices (m-by-n matrices for which m ≠ n) do not have a determinant.\n";
                }
            }
            if (bM1 == false)
            {
                Result.Text = Result.Text + "Matrix 1 is empty!!\n";
            }
            if (bM2 == false)
            {
                Result.Text = Result.Text + "Matrix 2 is empty!!\n";
            }
        }

        /******************************************************************************/
        /*                                 反矩陣                                     */
        /******************************************************************************/
        private void Inverse_Click(object sender, EventArgs e)
        {
            if (bM3 == true)                                                //若有舊的M3矩陣資源未釋放 先釋放資源以防錯誤
            {
                M3.Dispose();
                bM3 = false;
            }

            if (bM1 == true)
            {
                if ((int)M1.Cols == (int)M1.Rows)                           //確認矩陣row=col 要正方形矩陣才能計算反矩陣
                {
                    M3 = new Matrix<float>((int)M1.Rows, (int)M1.Cols);
                    bM3 = true;
                    if (comboBox1.SelectedIndex == 0)
                    {
                        CvInvoke.cvInvert(M1, M3, Emgu.CV.CvEnum.SOLVE_METHOD.CV_LU);
                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        CvInvoke.cvInvert(M1, M3, Emgu.CV.CvEnum.SOLVE_METHOD.CV_SVD_SYM);
                    }
                    else if (comboBox1.SelectedIndex == 3)
                    {
                        CvInvoke.cvInvert(M1, M3, Emgu.CV.CvEnum.SOLVE_METHOD.CV_SVD);
                    }
                    Result.Text = Result.Text + "Matrix 1's Inverse:\n";
                    PrintMatrix(M3, Result);
                }
                else {
              //      Result.Text = Result.Text + "Matrix1 is a Non-square matrix.\nNon-square matrices (m-by-n matrices for which m ≠ n) do not have an inverse.\n";
                    M3 = new Matrix<float>((int)M1.Rows, (int)M1.Cols);
                    bM3 = true;
                    M4 = M1.Transpose();
                    M3 = M4 * M1;
                    CvInvoke.cvInvert(M3, M3, Emgu.CV.CvEnum.SOLVE_METHOD.CV_SVD);
                    M3 = M3 * M4;
                    Result.Text = Result.Text + "Matrix 2's Inverse:\n";
                    PrintMatrix(M3, Result);
                }
            }
            if (bM2 == true)
            {
                if ((int)M2.Cols == (int)M2.Rows)
                {
                    M3 = new Matrix<float>((int)M2.Rows, (int)M2.Cols);
                    bM3 = true;
                    if (comboBox1.SelectedIndex == 0)
                    {
                        CvInvoke.cvInvert(M2, M3, Emgu.CV.CvEnum.SOLVE_METHOD.CV_LU);
                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        CvInvoke.cvInvert(M2, M3, Emgu.CV.CvEnum.SOLVE_METHOD.CV_SVD_SYM);
                    }
                    else if (comboBox1.SelectedIndex == 3)
                    {
                        CvInvoke.cvInvert(M2, M3, Emgu.CV.CvEnum.SOLVE_METHOD.CV_SVD);
                    }
                    Result.Text = Result.Text + "Matrix 2's Inverse:\n";
                    PrintMatrix(M3, Result);
                }
                else
                {
              //     Result.Text = Result.Text + "Matrix2 is a Non-square matrix.\nNon-square matrices (m ≠ n) do not have an inverse.\n";
                    M3 = new Matrix<float>((int)M2.Rows, (int)M2.Cols);
                    bM3 = true;
                    M4 = M2.Transpose();
                    M3 = M4 * M2;
                    CvInvoke.cvInvert(M3, M3, Emgu.CV.CvEnum.SOLVE_METHOD.CV_SVD);
                    M3 = M3 * M4;
                    Result.Text = Result.Text + "Matrix 2's Inverse:\n";
                    PrintMatrix(M3, Result);
                }
            }
            if (bM1 == false)
            {
                Result.Text = Result.Text + "Matrix 1 is empty!!\n";
            }
            if (bM2 == false)
            {
                Result.Text = Result.Text + "Matrix 2 is empty!!\n";
            }
        }
    }
}
