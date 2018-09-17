// ****************************************
// FileName:Player.cs 
// Description:玩家对象类
// Tables:Nothing
// Author:Jordan Zuo
// Create Date:2014-03-12
// Revision History:
//      1. 修改时间：2014-03-20 17:45:32
//          修改人： byron , zhaoxin
//          原因：添加属性
//      2. 修改时间：2015-08-07 15:43:00
//          修改人：Jordan zuo
//          原因：重构代码
// ****************************************

using System;

namespace PycMono.Project.Common
{
    /// <summary>
    /// 玩家对象类
    /// </summary>
    public sealed class Player
    {
        #region 字段

        private Guid mID = Guid.Empty;
        private Int32 mPartnerID = 0;
        private Int32 mServerID = 0;
        private string mUserID = String.Empty;
        private string mName = String.Empty;

        #endregion

        #region 属性

        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid ID
        {
            get { return mID; }
            set
            {
                if (mID != value)
                {
                    mID = value;
                }
            }
        }

        /// <summary>
        /// 合作商Id
        /// </summary>
        public Int32 PartnerID
        {
            get { return mPartnerID; }
            set
            {
                if (mPartnerID != value)
                {
                    mPartnerID = value;
                }
            }
        }

        /// <summary>
        /// 服务器Id
        /// </summary>
        public Int32 ServerID
        {
            get { return mServerID; }
            set
            {
                if (mServerID != value)
                {
                    mServerID = value;
                }
            }
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserID
        {
            get { return mUserID; }
            set
            {
                if (mUserID != value)
                {
                    mUserID = value;
                }
            }
        }

        /// <summary>
        /// 玩家名称
        /// </summary>
        public string Name
        {
            get { return mName; }
            set
            {
                if (mName != value)
                {
                    mName = value;
                }
            }
        }

        #endregion
    }
}

