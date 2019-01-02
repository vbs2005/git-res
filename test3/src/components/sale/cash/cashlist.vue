<template>
  <div style="background-color: bisque; border:1px solid black ">
    <div class="cash-title">
        <span>回款管理</span>
    </div>

    <div class="cash-search">
      <span class="fs14 mr15">筛选:</span>
      <el-button type="primary" size="mini" icon="el-icon-search"></el-button>
      <el-button type="danger" size="medium">重置</el-button>
    </div >

    <div class="cash-table" >
        <el-table :data="cashList" border style="width: 100%" highlight-current-row
                  v-loading="loading" @click="getDetail(currentRow.cashId)">
          <el-table-column label="编号" width="180">
            <template slot-scope="scope">
              <a href="javascript:">{{scope.row.cashId}}</a>
            </template>
          </el-table-column>

          <el-table-column label="订单编号" width="160">
            <template slot-scope="scope">
              <span>{{ scope.row.ordId }}</span>
            </template>
          </el-table-column>

          <el-table-column label="客户名称" width="150">
          <template slot-scope="scope">
            <span>{{scope.row.userName }}</span>
          </template>
        </el-table-column>

          <el-table-column label="回款时间" min-width="160">
            <template slot-scope="scope">
              <span>{{new Date(scope.row.cashDate).toLocaleDateString().replace(/\//g, '-')}}&nbsp;{{new Date(scope.row.cashDate).toTimeString().split(' ')[0]}}</span>
            </template>
          </el-table-column>

          <el-table-column label="回款状态">
            <template slot-scope="scope">
              <span v-if="scope.row.cashStatus === 0">待回款</span>
              <span v-if="scope.row.cashStatus === 1">部分回款</span>
              <span v-if="scope.row.cashStatus === 2" style="color: green">已回款</span>
              <span v-if="scope.row.cashStatus === 3" style="color: red">已取消</span>
            </template>
          </el-table-column>
          <el-table-column label="回款金额(元)" width="150">
                <template slot-scope="scope">
                <span>{{ (scope.row.cashAmount / 100).toFixed(2) }}</span>
                </template>
          </el-table-column>


      </el-table>
    </div>
    <div class="detail">
      <!-- 详情弹窗 -->
      <div class="cash-content-box" v-if="contentShow" :class="{'show-box':contentClass}">
        <div class="cash-main">
          <p class="content-close"><a href="javascript:;" @click="hideContentDo">X</a></p>
          <div class="content-top">
            <h2>回款金额：{{(castInfo.cashAmount / 100).toFixed(2)}}元</h2>

            <p>
              回款编号：{{castInfo.cashId}}<span>|</span>回款日期：{{new Date(castInfo.cashDate).toLocaleDateString().replace(/\//g, '-')
              }}</p>
          </div>
          <div class="content-type">
            <ul class="flli">
              <li v-if="castInfo.cashStatus===0">回款状态：待回款</li>
              <li v-else-if="castInfo.cashStatus===1">回款状态：部分回款</li>
              <li v-else-if="castInfo.cashStatus===3">回款状态：已取消</li>
              <li v-else>回款状态：已回款</li>
            </ul>
            <div class="clear"></div>
          </div>
          <div class="content-tab">
            <ul class="flli">
              <li class="cur">概要</li>
            </ul>
            <div class="clear"></div>
          </div>
          <div class="content-info">
            <h3 class="content-title">基本信息</h3>
            <table class="content-table">
              <tr>
                <td>回款编号</td>
                <td><span>{{castInfo.cashId}}</span></td>
              </tr>
              <tr>
                <td>回款金额</td>
                <td>{{(castInfo.cashAmount / 100).toFixed(2)}}元</td>
              </tr>
              <tr>
                <td>账户类型</td>
                <td v-if="castInfo.cashAccountType">
                  {{castInfo.cashAccountType===1?'公账':'其他账户'}}
                </td>
                <td v-else-if="castInfo.cashAccountType===0">其他账户</td>
                <td v-else>--</td>
              </tr>
              <tr>
                <td>回款类型</td>
                <td v-if="castInfo.cashType===0">立即回款</td>
                <td v-if="castInfo.cashType===1">定期回款</td>
                <td v-if="castInfo.cashType === '--'">--</td>
              </tr>
              <tr>
                <td>回款日期</td>
                <td>{{new Date(castInfo.cashDate).toLocaleDateString().replace(/\//g, '-') }}</td>
              </tr>
              <tr>
                <td>回款状态</td>
                <td v-if="castInfo.cashStatus===0">待回款</td>
                <td v-if="castInfo.cashStatus===1">部分回款</td>
                <td v-if="castInfo.cashStatus===2">已回款</td>
                <td v-if="castInfo.cashStatus===3">已取消</td>
                <td v-if="castInfo.cashStatus === '--'">--</td>
              </tr>
              <tr>
                <td>回款渠道</td>
                <td v-if="castInfo.payChannel === 'zfb'">支付宝</td>
                <td v-if="castInfo.payChannel === 'wx_pay'">微信支付</td>
                <td v-if="castInfo.payChannel === 'bank_trans'">银行转账</td>
                <td v-if="castInfo.payChannel === 'easy_pay'">苏宁易付宝</td>
                <td v-if="castInfo.payChannel === 'yi_ji_fu'">易极付</td>
                <td v-if="castInfo.payChannel === 'ljl_pay'">蓝金灵支付</td>
                <td v-if="castInfo.payChannel === '--'">--</td>
              </tr>
              <tr>
                <td>备注</td>
                <td>{{castInfo.remark}}</td>
              </tr>
            </table>
          </div>
        </div>
      </div>
      <!--遮罩层-->
      <div class="cash-content" v-show="contentShow" @click="hideContentDo"></div>
    </div>
  </div>
</template>

<script>
  let http_url={
    list:'http://xxx.xxx.com/xxx/cash/list',
    detail:'http://xxx.xxx.com/xxx/cash/detail'
  }

    export default {
        name: "cashlist",
      data(){
          return{
            contentShow:false,
            cashInfo:{
              "cashId":null,
              "ordId":null,
              "cashAmount":null,
              "currency":"CNY",
              "currentUnit":"fen",
              "cashDate":null,
              "createTime":null,
              "cashType":0,
              "payChannel":"bank_trans",
              "payNo":null,
              "payType":null,
              "cashStatus":2,
              "logisticsIds":"",
              "ready":true,
              "cashAccountType":2,
              "reMark":"",
              "isDelete":0,
              "ordPayTotal":5200,
              "payTime":null,
              "netDays":0,
              "Id":null

            },
            loading:false,
            pageSize:10,//每页条数
            allCount:0,//记录总数
            currentPage:1,//当前页码
            keyFrom:{
              "ordId":null,
              "cashId":null,
              "custoName":null,
              "cashType":null,
              "cashStatus":null,
              "userName":null,
              "useMobile":null
            },//搜索字段

            cashList: [
                {
                      "cashId": "M2017062900049001",
                      "ordId": "O2017062900075030",
                      "cashType": 0,
                      "payChannel": null,
                      "payType": null,
                      "cashStatus": 0,
                      "custoName": "UFO",
                      "userName": "陈",
                      "userMobile": "16936025651",
                      "merchandisers": "文",
                      "cashAmount": 1832500,
                      "cashDate": 1498718850000,
                      "cashAccountType": 0,
                      "payNo": null
                    },
                {
                  "cashId": "M2017062900049002",
                  "ordId": "O2017062900075031",
                  "cashType": 0,
                  "payChannel": null,
                  "payType": null,
                  "cashStatus": 0,
                  "custoName": "UFO",
                  "userName": "天使",
                  "userMobile": "13926085651",
                  "merchandisers": "乐",
                  "cashAmount": 1832500,
                  "cashDate": 14987188558400,
                  "cashAccountType": 0,
                  "payNo": null
              }
            ]//列表数组(现在是准备请求接口，不需要模拟的数据，所以设置一个空数组)

      }
      },
      methods: {
        /**
         * 清除对象中值为空的属性
         */
        filterParams(obj) {
          let _form = obj, _newPar = {}, testStr;
          //遍历对象
          for (let key in _form) {
            testStr = null;
            //如果属性的值不为空。
            //注意，不要这样判断if (_form[key])。因为有些属性的值可能为0，到时候就会被过滤掉
            if (_form[key] !== null && _form[key] !== "") {
              //把值添加进新对象里面
              _newPar[key] = _form[key].toString()
            }
          }
          //返回对象
          return _newPar;
        },
        getList(){
          //显示加载中提示
         //this.loading=true;
          //过滤搜索字段值为空的属性，然后对象合并，合并上页码。
          let _par = Object.assign(this.filterParams(this.keyFrom), {
            pageNo: this.currentPage,
            pageSize: this.pageSize
          });
          this.$http.get(http_url.list, {
            params: _par
          }).then(function (res) {
            res=res.body;
            //如果请求成功了，这接口code为0代表请求成功。具体怎样判断还需要看接口
            if(res.code===0){
              //设置列表数据
              this.cashList = res.datas.entityList;
              /*this.cashList.map(function (value) {
                for (let key in value) {
                  //不要if(value[key])判断，有的值可能是0，会把0过滤
                  if (value[key] === null || value[key] === '') {
                    value[key] = '--'
                  }
                }
              });
              //关闭加载中提示
              this.loading=false;
              */
            }
            else{
              this.$message.error(res.msg);
            }
          });
        },
        getDetail: function (id) {
          //contentShow控制详情页和遮罩层的显示，contentClass控制详情页的动画，castInfo为记录请求回来的数据。大家要在data上面声明哦！
          //也是同样的处理方式，区别就是this.$loading是element提供的全局组件，效果就是整个屏幕显示加载中。
          let loadingContent = this.$loading({
            text: '正在加载中...'
          });
          //显示详情
          this.$http.get(http_url.detail, {
            params: {
              cashId: id
            }
          }).then(function (res) {
            res = res.body;
            if (res.code === 0) {
              this.castInfo = res.datas.castInfo;
              //关闭加载中提示
              loadingContent.close();
              //显示详情的div
              this.contentShow = true;
              //为了确保看到动画效果，所以让遮罩层和详情的DIV先显示，再执行this.contentClass = true;让详情DIV从右至左的出现
              setTimeout(() => {
                this.contentClass = true;
              })
            }
            else {
              this.$message.error(res.msg);
            }
          });
        },
        /**
         * @description 隐藏详情
         */
        hideContentDo(){
          //先执行this.contentClass = false。让详情DIV从左至右回去，等回去了之后，再执行this.contentShow = false;在隐藏div,否则会看不到动画效果。设置的时间，就是当时动画的时间！transition: transform 1s
          this.contentClass = false;
          setTimeout(() => {
            this.contentShow = false;
          }, 1000)
        },
      },
      mounted(){
        this.getList();
      },
    }
</script>

<style lang="scss" scoped>

  .cash-title{
        text-align: left;



  }
  .cash-search{
        text-align: left;

  }
  .detail{
      width: 700px;
      z-index: 1006;
      position: fixed;
      top: 0;
      right: 0;
      transform: translate3d(700px, 0, 0);
      height: 100%;
      background: #535968;
      transition: transform 1s;
  }
</style>
