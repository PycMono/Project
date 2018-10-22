//***********************************************************************************
// 文件名称：ResourceBase.cs
// 功能描述：资源处理父类
// 数据表：无
// 作者：pyc
// 日期：2018/10/21 11:16:37
// 修改记录：
//***********************************************************************************

using System;

namespace PycMono.Project.Model
{
    using PycMono.Project.Model.Enum;

    /// <summary>
    /// 游戏资源对象
    /// </summary>
    public class GameResourceObject
    {
        #region 变量

        private ResourceTypeSubEnum mResourceTypeSub;
        private Int32 mModelId;
        private Int64 mCount;
        private Guid mEntityId = Guid.Empty;
        private Int32? mLastTime = null;
        private DateTime? mBeginTime = null;
        private DateTime? mEndTime = null;

        private Boolean mIsLock = false;//资源默认可以修改，如果设置为不可修改，则后续改变资源数值将会抛出异常

        #endregion

        #region 属性

        /// <summary>
        /// 游戏资源类型枚举
        /// </summary>
        public ResourceTypeSubEnum ResourceTypeSub {
            get { return mResourceTypeSub; }
            set {
                if (mResourceTypeSub != value)
                {
                    CheckStatus();
                    mResourceTypeSub = value;
                }
            }
        }

        /// <summary>
        /// 资源实体Id
        /// </summary>
        public Guid EntityId {
            get { return mEntityId; }
            set {
                if (mEntityId != value)
                {
                    if (mEntityId != Guid.Empty)
                        CheckStatus();
                    mEntityId = value;
                }
            }
        }

        /// <summary>
        /// 最终获得的资源模型Id
        /// </summary>
        public Int32 ModelId {
            get { return mModelId; }
            set {
                if (mModelId != value)
                {
                    CheckStatus();
                    mModelId = value;
                }
            }
        }

        /// <summary>
        /// 资源数量
        /// </summary>
        public Int64 Count {
            get { return mCount; }
            set {
                if (value <= 0)
                    throw new ArgumentException("Count 必须为正整数。");

                if (mCount != value)
                {
                    CheckStatus();
                    mCount = value;
                }
            }
        }

        /// <summary>
        /// 资源有效持续时间，单位小时
        /// </summary>
        public Int32? LastTime {
            get { return mLastTime; }
            set {
                if (mLastTime != value)
                {
                    CheckStatus();
                    mLastTime = value;
                }
            }
        }

        /// <summary>
        /// 资源有效开始时间
        /// </summary>
        public DateTime? BeginTime {
            get { return mBeginTime; }
            set {
                if (mBeginTime != value)
                {
                    CheckStatus();
                    mBeginTime = value;
                }
            }
        }

        /// <summary>
        /// 资源有效结束时间
        /// </summary>
        public DateTime? EndTime {
            get { return mEndTime; }
            set {
                if (mEndTime != value)
                {
                    CheckStatus();
                    mEndTime = value;
                }
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 创建资源
        /// </summary>
        /// <param name="subType">类型</param>
        /// <param name="count">数量</param>
        public GameResourceObject(ResourceTypeSubEnum subType, Int64 count)
            : this(subType, 0, count)
        {

        }

        /// <summary>
        /// 创建资源
        /// </summary>
        /// <param name="subType">类型</param>
        /// <param name="modelId">模型id</param>
        /// <param name="count">数量</param>
        public GameResourceObject(ResourceTypeSubEnum subType, Int32 modelId, Int64 count)
            : this(subType, modelId, count, true)
        {

        }

        /// <summary>
        /// 创建资源
        /// </summary>
        /// <param name="subType">类型</param>
        /// <param name="modelId">模型id</param>
        /// <param name="count">数量</param>
        public GameResourceObject(ResourceTypeSubEnum subType, Int32 modelId, Int64 count, Guid entityId)
            : this(subType, modelId, count, entityId, null, null, null, true)
        {

        }

        /// <summary>
        /// 创建资源
        /// </summary>
        /// <param name="subType">类型</param>
        /// <param name="modelId">模型id</param>
        /// <param name="count">数量</param>
        /// <param name="isLock">是否锁定资源</param>
        public GameResourceObject(ResourceTypeSubEnum subType, Int32 modelId, Int64 count, Boolean isLock)
            : this(subType, modelId, count, Guid.Empty, null, null, null, isLock)
        {

        }

        /// <summary>
        /// 创建资源
        /// </summary>
        /// <param name="subType">类型</param>
        /// <param name="modelId">模型id</param>
        /// <param name="count">数量</param>
        /// <param name="entityId">资源id</param>
        /// <param name="isLock">是否锁定资源</param>
        public GameResourceObject(ResourceTypeSubEnum subType, Int32 modelId, Int64 count, Guid entityId, Boolean isLock)
            : this(subType, modelId, count, entityId, null, null, null, isLock)
        {

        }

        /// <summary>
        /// 创建资源
        /// </summary>
        /// <param name="subType">类型</param>
        /// <param name="modelId">模型id</param>
        /// <param name="count">数量</param>
        /// <param name="entityId">实体id</param>
        /// <param name="lastTime">持续时间</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">EndTime</param>
        /// <param name="isLock">所否锁定资源</param>
        public GameResourceObject(ResourceTypeSubEnum subType, Int32 modelId, Int64 count, Guid entityId, Int32? lastTime, DateTime? beginTime, DateTime? endTime, Boolean isLock)
        {
            this.ResourceTypeSub = subType;
            this.ModelId = modelId;
            this.Count = count;
            this.EntityId = entityId;
            this.LastTime = lastTime;
            this.BeginTime = beginTime;
            this.EndTime = endTime;
            this.mIsLock = isLock;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 锁定资源，锁定之后资源不可修改
        /// </summary>
        public void Lock()
        {
            mIsLock = true;
        }

        /// <summary>
        /// 解除资源锁定,方便修改资源
        /// </summary>
        public void UnLock()
        {
            mIsLock = false;
        }

        /// <summary>
        /// 判断资源状态，如果为不可修改状态，则会抛出异常
        /// </summary>
        private void CheckStatus()
        {
            if (mIsLock)
                throw new NotSupportedException("该资源已经被锁定，不可再修改。");
        }

        /// <summary>
        /// 克隆资源
        /// </summary>
        /// <param name="isLogk">是否锁定资源</param>
        /// <returns>新资源</returns>
        public GameResourceObject Clone(Boolean isLogk = false)
        {
            return new GameResourceObject(this.ResourceTypeSub, this.ModelId, this.Count, this.EntityId, this.LastTime, this.BeginTime, this.EndTime, isLogk);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            var other = (GameResourceObject)obj;
            if (this.ResourceTypeSub != other.ResourceTypeSub)
                return false;

            if (this.ModelId != other.ModelId)
                return false;

            if (this.EntityId != other.EntityId)
                return false;

            if (this.LastTime != other.LastTime)
                return false;

            if (this.BeginTime != other.BeginTime)
                return false;

            if (this.EndTime != other.EndTime)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return ResourceTypeSub.GetHashCode() + EntityId.GetHashCode() + ModelId.GetHashCode() + Count.GetHashCode();
        }

        public static Boolean operator ==(GameResourceObject a, GameResourceObject b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static Boolean operator !=(GameResourceObject a, GameResourceObject b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 两个资源相加
        /// </summary>
        /// <param name="a">资源A</param>
        /// <param name="b">资源B</param>
        /// <returns>一个全新的资源C，资源状态会被锁定</returns>
        public static GameResourceObject operator +(GameResourceObject a, GameResourceObject b)
        {
            if (a.Equals(b) == false)
                throw new Exception("资源不同，不可以进行+操作");

            return new GameResourceObject(a.ResourceTypeSub, a.ModelId, a.Count + b.Count, a.EntityId, a.LastTime, a.BeginTime, a.EndTime, true);
        }

        /// <summary>
        /// 两个资源相减
        /// </summary>
        /// <param name="a">资源A</param>
        /// <param name="b">资源B</param>
        /// <returns>一个全新的资源C，资源状态会被锁定；如果A资源和B资源数量相等，则会返回null</returns>
        public static GameResourceObject operator -(GameResourceObject a, GameResourceObject b)
        {
            if (a.Equals(b) == false)
                throw new Exception("资源不同，不可以进行+操作");

            if (a.Count < b.Count)
                throw new Exception("a资源数量小于b资源，不可以进行相减操作");

            if (a.Count == b.Count)
                return null;

            return new GameResourceObject(a.ResourceTypeSub, a.ModelId, a.Count - b.Count, a.EntityId, a.LastTime, a.BeginTime, a.EndTime, true);
        }

        #endregion
    }
}