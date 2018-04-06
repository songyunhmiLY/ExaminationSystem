﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ExaminationClient {
    public partial class ExaminationSelectCtrl : ExaminationClient.BaseCtrl {
        public ExaminationSelectCtrl() {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.  
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲 
        }

        public delegate void StartExamEventDelegate(string subType,string subLevel,int subNum);
        public StartExamEventDelegate StartExamEventHandel;

        /// <summary>
        /// 初始化题目类型
        /// </summary>
        private void InitSubTyep() {
            var list = ServiceWindow.Service.GetSubjectTypes();
            if (list ==null||list.Length == 0) {
                this.cmboxSubType.DataSource = null;
                return;
            }
            this.cmboxSubType.DataSource = list;
            this.cmboxSubType.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化题目等级
        /// </summary>
        private void InitSubLevel() {
            var list = ServiceWindow.Service.GetSubjectLevels();
            if (list == null || list.Length == 0) {
                this.cmboxSubLevel.DataSource = null;
                return;
            }
            this.cmboxSubLevel.DataSource = list;
            this.cmboxSubLevel.SelectedIndex = 0;
        }

        /// <summary>
        /// 输入验证
        /// </summary>
        /// <returns></returns>
        private bool InputValidator() {
            if (!Regex.IsMatch(this.txtBoxSubNumber.Text.Trim(), "^\\d+$")) {
                MessageBox.Show("题目数量输入不正确！");
                return false;
            }
            return true;
        }

        #region 事件
        private void ExaminationCtrl_Load(object sender, EventArgs e) {
            InitSubTyep();
            InitSubLevel();
        }

        private void txtBoxSubNumber_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar >= 47 && e.KeyChar <= 58) {
                e.Handled = false;
            }
        }

        private void btnStart_Click(object sender, EventArgs e) {
            if (!InputValidator()) {
                return;
            }
            if (StartExamEventHandel != null) {
                StartExamEventHandel(this.cmboxSubType.SelectedValue.ToString(), this.cmboxSubLevel.SelectedValue.ToString(),
                    Convert.ToInt32(this.txtBoxSubNumber.Text));
            }
        }
        #endregion 
         
    }
}