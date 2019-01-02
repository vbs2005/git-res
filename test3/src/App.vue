<template>
  <div id="app">
    <el-row>
    <div style="background-color: darkgrey">
      <topbar-com></topbar-com>
    </div>
    </el-row>
    <el-row>
      <el-col :span="5" >

       <div>
         <snav-component></snav-component>

       </div>

      </el-col>
      <el-col :span="19" >

      <div>
        <router-view></router-view>
      </div>

      </el-col>

    </el-row>

  </div>
</template>

<script>
  import topbarcom from '@/components/TopBar.vue'
  import snavcomponent from '@/components/snav-component.vue'


export default {
  name: 'App',
  components: {
    'topbar-com': topbarcom,
    'snav-component': snavcomponent
  }


}
</script>

<style>
    #app {
        font-family: 'Avenir', Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        color: #2c3e50;
        margin-top:3px;
        width: 100%;
    }
    el-row{

    }

</style>
