<template>
  <v-treeview
    activatable
    :categories="categories"
  ></v-treeview>
</template>
<script>
  export default {
     props: {
      categories:{
      type: Object,
      default: () => {}

    }
  }
  }
</script>