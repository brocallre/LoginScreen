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

                // 비밀번호 칸에 글자를 입력할 때부터 별표 표시
                if (txt == txtPassword)
                {
                    txt.PasswordChar = '*';
                }
            }
        }

        private void TxtId_GotFocus(object sender, EventArgs e)
        {
            RemovePlaceholder(txtId, idPlaceholder);
        }

        private void TxtId_LostFocus(object sender, EventArgs e)
        {
            SetPlaceholder(txtId, idPlaceholder);
        }

        private void TxtPassword_GotFocus(object sender, EventArgs e)
        {
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
                MessageBox.Show("로그인 성공!", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("아이디 또는 비밀번호가 틀렸습니다.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
