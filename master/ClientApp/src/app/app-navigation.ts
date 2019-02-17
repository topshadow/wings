export const navigation = [
  {
    text: '机构与权限',
    //path: '/admin/rbac',
    icon: 'home',
    items: [
      { text: "组织管理", path: "/admin/rbac/org" },
      { text: "菜单管理", path: "/admin/rbac/menu" },
      { text: "角色管理", path: "/admin/rbac/role" },
      { text: "用户管理", path: "/admin/rbac/user" }
    ]
  },
  {
    text: "集群",
    icon: "computer",
    items: [
      { text: "Wokers", path: "/admin/worker/worker" },
      {
        text: "基础",
        items: [{ path: "/admin/worker/basic/position" ,text:"地点"}]
      }
    ]
  },
  {
    text: 'Examples',
    icon: 'folder',
    items: [
      {
        text: 'Profile',
        path: '/profile'
      },
      {
        text: 'Display Data',
        path: '/display-data'
      }
    ]
  }
];
