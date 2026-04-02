using System;
using System.Drawing;
using System.Windows.Forms;

namespace LoginScreen
{
    public partial class Form1 : Form
    {
        // 텍스트박스 기본 안내문구(Placeholder) 설정
        private string idPlaceholder = "아이디를 입력하세요";
        private string pwPlaceholder = "비밀번호를 입력하세요";
        private bool isPasswordShowing = false; // 비밀번호 보기 상태

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 프로그램이 켜졌을 때 처음으로 나올 화면 설정
            SetPlaceholder(txtId, idPlaceholder);
            SetPlaceholder(txtPassword, pwPlaceholder);

            // 텍스트박스에 마우스 클릭(포커스) 이벤트 연결
            txtId.GotFocus += TxtId_GotFocus;
            txtId.LostFocus += TxtId_LostFocus;

            txtPassword.GotFocus += TxtPassword_GotFocus;
            txtPassword.LostFocus += TxtPassword_LostFocus;

            // 시작하자마자 텍스트박스에 커서가 가는 것을 방지하기 위해 로고(레이블)에 포커스를 줌
            this.ActiveControl = lblTitle;
        }

        // 안내문구를 보여주는 함수
        private void SetPlaceholder(TextBox txt, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.Text = placeholder;
                txt.ForeColor = Color.Gray;

                // 비밀번호 칸일 경우 힌트 글씨가 별표로 바뀌지 않게 해제
                if (txt == txtPassword)
                {
                    txt.PasswordChar = '\0';
                }
            }
        }

        // 안내문구를 지우는 함수
        private void RemovePlaceholder(TextBox txt, string placeholder)
        {
            if (txt.Text == placeholder)
            {
                txt.Text = "";
                txt.ForeColor = Color.Black;

                // 비밀번호 칸에 글자를 입력할 때부터 별표 표시 (보기 모드가 아닐 때만)
                if (txt == txtPassword && !isPasswordShowing)
                {
                    txt.PasswordChar = '*';
                }
            }
        }

        private void TxtId_GotFocus(object sender, EventArgs e)
        {
            lblError.Visible = false; // 다시 입력하려고 하면 에러 메시지 숨김
            RemovePlaceholder(txtId, idPlaceholder);
        }

        private void TxtId_LostFocus(object sender, EventArgs e)
        {
            SetPlaceholder(txtId, idPlaceholder);
        }

        private void TxtPassword_GotFocus(object sender, EventArgs e)
        {
            lblError.Visible = false; // 다시 입력하려고 하면 에러 메시지 숨김
            RemovePlaceholder(txtPassword, pwPlaceholder);
        }

        private void TxtPassword_LostFocus(object sender, EventArgs e)
        {
            SetPlaceholder(txtPassword, pwPlaceholder);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string pw = txtPassword.Text;

            // 아직 아무것도 입력하지 않은 상태(힌트 문구)라면 빈 값으로 인식
            if (id == idPlaceholder) id = "";
            if (pw == pwPlaceholder) pw = "";

            // 아이디와 비밀번호가 모두 맞는지 확인 (&& 연산자 사용)
            if (id == "admin" && pw == "1234")
            {
                lblError.Visible = false;
                MessageBox.Show("로그인 성공!", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblError.Text = "아이디 또는 비밀번호가 틀렸습니다.";
                lblError.Visible = true;
            }
        }

        private void btnShowPw_Click(object sender, EventArgs e)
        {
            // 힌트 상태일 때는 토글 동작 안 함
            if (txtPassword.Text == pwPlaceholder || string.IsNullOrWhiteSpace(txtPassword.Text))
                return;

            isPasswordShowing = !isPasswordShowing;

            if (isPasswordShowing)
            {
                txtPassword.PasswordChar = '\0'; // 비밀번호 보이기
                btnShowPw.Text = "숨김";
            }
            else
            {
                txtPassword.PasswordChar = '*'; // 비밀번호 숨기기
                btnShowPw.Text = "보기";
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            // 엔터 키를 누르면 패스워드 입력창으로 포커스 이동
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // 삑 소리 제거
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            // 엔터 키를 누르면 로그인 버튼 클릭
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnLogin.PerformClick();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // 텍스트박스 초기화 및 힌트 상태로 되돌리기
            txtId.Text = "";
            txtPassword.Text = "";
            
            isPasswordShowing = false;
            btnShowPw.Text = "보기";
            lblError.Visible = false;

            SetPlaceholder(txtId, idPlaceholder);
            SetPlaceholder(txtPassword, pwPlaceholder);

            // 포커스 로고로 이동
            this.ActiveControl = lblTitle;
        }
    }
}
