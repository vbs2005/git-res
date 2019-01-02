import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import VueResource from 'vue-resource'
import Cashlist from '@/components/sale/cash/cashlist.vue'
import Invoicelist from '@/components/sale/invoice/invoicelist.vue'

Vue.use(Router)
Vue.use(VueResource)



export default new Router({
 routes: [
    {
      path: '/index',
      name: 'helloworld',
      component:HelloWorld,
      alias:'/'   //redirect改变重定向的地址以及内容。alias只改变重定向的内容，不改变地址。
    },
    {
      path:'/cash/cashList',
      name:'cashlist',
      component:Cashlist
    },
   {
     path:'/invoice/invoiceWriteList',
     name:'invoicelist',
     component: Invoicelist
   }
  ],

})
