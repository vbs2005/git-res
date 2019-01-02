<template>
  <div style="background-color: bisque; border:1px solid black ">
    <div class="cash-title">
      <span>开票管理</span>
    </div>

    <div class="cash-search">
      <span class="fs14 mr15">筛选:</span>
      <el-button type="primary" size="small" icon="el-icon-search"></el-button>
      <el-button type="danger" size="small">重置</el-button>
    </div>

    <div class="cash-table">
      <el-table :data="cashList" border style="width: 100%" highlight-current-row>
        <el-table-column label="编号" width="180">
          <template slot-scope="scope">
            <a href="javascript:">{{scope.row.cashId}}</a>
          </template>
        </el-table-column>

        <el-table-column label="客户名称" width="200">
          <template slot-scope="scope">
            <span>{{scope.row.userName }}</span>
          </template>
        </el-table-column>
      </el-table>
    </div>
  </div>
</template>

<script>
  export default {
    name: "cashlist",
    data(){
      return{
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
        ]
      }
    }
  }
</script>

<style lang="scss" scoped>

  .cash-title{
    text-align: left;



  }
  .cash-search{
    text-align: left;

  }
</style>
