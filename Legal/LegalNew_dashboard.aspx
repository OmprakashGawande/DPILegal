﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="LegalNew_dashboard.aspx.cs" Inherits="Legal_LegalNew_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../NewDashboard/js/layout.js"></script>
    <!-- Bootstrap Css -->
    <link href="../NewDashboard/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Icons Css -->
    <link href="../NewDashboard/css/icons.min.css" rel="stylesheet" />
    <!-- App Css-->
    <link href="../NewDashboard/css/app.min.css" rel="stylesheet" />
    <!-- custom Css-->
    <link href="../NewDashboard/css/custom.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">

                <div class="project-wrapper">
                    <div class="row">
                        <div class="col-xl-3 col-md-6">
                            <!-- card -->
                            <div class="card card-animate">
                                <div class="card-body">
                                    <div class="d-flex align-items-center">
                                        <div class="flex-grow-1 overflow-hidden">
                                            <p class="text-uppercase fw-medium text-muted text-truncate mb-0">Total Earnings</p>
                                        </div>
                                        <div class="flex-shrink-0">
                                            <h5 class="text-success fs-14 mb-0">
                                                <i class="ri-arrow-right-up-line fs-13 align-middle"></i>+16.24 %
                        </h5>
                                        </div>
                                    </div>
                                    <div class="d-flex align-items-end justify-content-between mt-4">
                                        <div>
                                            <h4 class="fs-22 fw-semibold ff-secondary mb-4">$<span class="counter-value" data-target="559.25">0</span>k </h4>
                                            <a href="#" class="text-decoration-underline">View net earnings</a>
                                        </div>
                                        <div class="avatar-sm flex-shrink-0">
                                            <span class="avatar-title bg-success-subtle rounded fs-3">
                                                <i class="bx bx-dollar-circle text-success"></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card body -->
                            </div>
                            <!-- end card -->
                        </div>
                        <!-- end col -->

                        <div class="col-xl-3 col-md-6">
                            <!-- card -->
                            <div class="card card-animate">
                                <div class="card-body">
                                    <div class="d-flex align-items-center">
                                        <div class="flex-grow-1 overflow-hidden">
                                            <p class="text-uppercase fw-medium text-muted text-truncate mb-0">Orders</p>
                                        </div>
                                        <div class="flex-shrink-0">
                                            <h5 class="text-danger fs-14 mb-0">
                                                <i class="ri-arrow-right-down-line fs-13 align-middle"></i>-3.57 %
                        </h5>
                                        </div>
                                    </div>
                                    <div class="d-flex align-items-end justify-content-between mt-4">
                                        <div>
                                            <h4 class="fs-22 fw-semibold ff-secondary mb-4"><span class="counter-value" data-target="36894">0</span></h4>
                                            <a href="#" class="text-decoration-underline">View all orders</a>
                                        </div>
                                        <div class="avatar-sm flex-shrink-0">
                                            <span class="avatar-title bg-info-subtle rounded fs-3">
                                                <i class="bx bx-shopping-bag text-info"></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card body -->
                            </div>
                            <!-- end card -->
                        </div>
                        <!-- end col -->

                        <div class="col-xl-3 col-md-6">
                            <!-- card -->
                            <div class="card card-animate">
                                <div class="card-body">
                                    <div class="d-flex align-items-center">
                                        <div class="flex-grow-1 overflow-hidden">
                                            <p class="text-uppercase fw-medium text-muted text-truncate mb-0">Customers</p>
                                        </div>
                                        <div class="flex-shrink-0">
                                            <h5 class="text-success fs-14 mb-0">
                                                <i class="ri-arrow-right-up-line fs-13 align-middle"></i>+29.08 %
                        </h5>
                                        </div>
                                    </div>
                                    <div class="d-flex align-items-end justify-content-between mt-4">
                                        <div>
                                            <h4 class="fs-22 fw-semibold ff-secondary mb-4"><span class="counter-value" data-target="183.35">0</span>M </h4>
                                            <a href="#" class="text-decoration-underline">See details</a>
                                        </div>
                                        <div class="avatar-sm flex-shrink-0">
                                            <span class="avatar-title bg-warning-subtle rounded fs-3">
                                                <i class="bx bx-user-circle text-warning"></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card body -->
                            </div>
                            <!-- end card -->
                        </div>
                        <!-- end col -->

                        <div class="col-xl-3 col-md-6">
                            <!-- card -->
                            <div class="card card-animate">
                                <div class="card-body">
                                    <div class="d-flex align-items-center">
                                        <div class="flex-grow-1 overflow-hidden">
                                            <p class="text-uppercase fw-medium text-muted text-truncate mb-0">My Balance</p>
                                        </div>
                                        <div class="flex-shrink-0">
                                            <h5 class="text-muted fs-14 mb-0">+0.00 %
                        </h5>
                                        </div>
                                    </div>
                                    <div class="d-flex align-items-end justify-content-between mt-4">
                                        <div>
                                            <h4 class="fs-22 fw-semibold ff-secondary mb-4">$<span class="counter-value" data-target="165.89">0</span>k </h4>
                                            <a href="#" class="text-decoration-underline">Withdraw money</a>
                                        </div>
                                        <div class="avatar-sm flex-shrink-0">
                                            <span class="avatar-title bg-primary-subtle rounded fs-3">
                                                <i class="bx bx-wallet text-primary"></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card body -->
                            </div>
                            <!-- end card -->
                        </div>
                        <!-- end col -->
                    </div>
                    <%--<div class="row">
                        <div class="col-md-4">
                            <div class="card card-animate">
                                <div class="card-body">
                                    <div class="d-flex align-items-center">
                                        <div class="avatar-sm flex-shrink-0">
                                            <span class="avatar-title bg-primary-subtle text-primary rounded-2 fs-2">
                                                <i data-feather="briefcase" class="text-primary"></i>
                                            </span>
                                        </div>
                                        <div class="flex-grow-1 overflow-hidden ms-3">
                                            <p class="text-uppercase fw-medium text-muted text-truncate mb-3">Active Projects</p>
                                            <div class="d-flex align-items-center mb-3">
                                                <h4 class="fs-4 flex-grow-1 mb-0"><span class="counter-value" data-target="825">0</span></h4>
                                                <span class="badge bg-danger-subtle text-danger fs-12"><i class="ri-arrow-down-s-line fs-13 align-middle me-1"></i>5.02 %</span>
                                            </div>
                                            <p class="text-muted text-truncate mb-0">Projects this month</p>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card body -->
                            </div>
                        </div>
                        <!-- end col -->

                        <div class="col-md-4">
                            <div class="card card-animate">
                                <div class="card-body">
                                    <div class="d-flex align-items-center">
                                        <div class="avatar-sm flex-shrink-0">
                                            <span class="avatar-title bg-warning-subtle text-warning rounded-2 fs-2">
                                                <i data-feather="award" class="text-warning"></i>
                                            </span>
                                        </div>
                                        <div class="flex-grow-1 ms-3">
                                            <p class="text-uppercase fw-medium text-muted mb-3">New Leads</p>
                                            <div class="d-flex align-items-center mb-3">
                                                <h4 class="fs-4 flex-grow-1 mb-0"><span class="counter-value" data-target="7522">0</span></h4>
                                                <span class="badge bg-success-subtle text-success fs-12"><i class="ri-arrow-up-s-line fs-13 align-middle me-1"></i>3.58 %</span>
                                            </div>
                                            <p class="text-muted mb-0">Leads this month</p>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card body -->
                            </div>
                        </div>
                        <!-- end col -->

                        <div class="col-md-4">
                            <div class="card card-animate">
                                <div class="card-body">
                                    <div class="d-flex align-items-center">
                                        <div class="avatar-sm flex-shrink-0">
                                            <span class="avatar-title bg-info-subtle text-info rounded-2 fs-2">
                                                <i data-feather="clock" class="text-info"></i>
                                            </span>
                                        </div>
                                        <div class="flex-grow-1 overflow-hidden ms-3">
                                            <p class="text-uppercase fw-medium text-muted text-truncate mb-3">Total Hours</p>
                                            <div class="d-flex align-items-center mb-3">
                                                <h4 class="fs-4 flex-grow-1 mb-0"><span class="counter-value" data-target="168">0</span>h <span class="counter-value" data-target="40">0</span>m</h4>
                                                <span class="badge bg-danger-subtle text-danger fs-12"><i class="ri-arrow-down-s-line fs-13 align-middle me-1"></i>10.35 %</span>
                                            </div>
                                            <p class="text-muted text-truncate mb-0">Work this month</p>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card body -->
                            </div>
                        </div>
                        <!-- end col -->
                    </div>--%>
                    <!-- end row -->
                    <br />
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-xl-12">
                            <div class="card">
                                <div class="card-header border-0 align-items-center d-flex">
                                    <h4 class="card-title mb-0 flex-grow-1">Projects Overview</h4>
                                    <div>
                                        <button type="button" class="btn btn-soft-secondary btn-sm material-shadow-none">
                                            ALL
                                               
                                        </button>
                                        <button type="button" class="btn btn-soft-secondary btn-sm material-shadow-none">
                                            1M
                                               
                                        </button>
                                        <button type="button" class="btn btn-soft-secondary btn-sm material-shadow-none">
                                            6M
                                               
                                        </button>
                                        <button type="button" class="btn btn-soft-primary btn-sm material-shadow-none">
                                            1Y
                                               
                                        </button>
                                    </div>
                                </div>
                                <!-- end card header -->

                                <div class="card-header p-0 border-0 bg-light-subtle">
                                    <div class="row g-0 text-center">
                                        <div class="col-6 col-sm-3">
                                            <div class="p-3 border border-dashed border-start-0">
                                                <h5 class="mb-1"><span class="counter-value" data-target="9851">0</span></h5>
                                                <p class="text-muted mb-0">Number of Projects</p>
                                            </div>
                                        </div>
                                        <!--end col-->
                                        <div class="col-6 col-sm-3">
                                            <div class="p-3 border border-dashed border-start-0">
                                                <h5 class="mb-1"><span class="counter-value" data-target="1026">0</span></h5>
                                                <p class="text-muted mb-0">Active Projects</p>
                                            </div>
                                        </div>
                                        <!--end col-->
                                        <div class="col-6 col-sm-3">
                                            <div class="p-3 border border-dashed border-start-0">
                                                <h5 class="mb-1">$<span class="counter-value" data-target="228.89">0</span>k</h5>
                                                <p class="text-muted mb-0">Revenue</p>
                                            </div>
                                        </div>
                                        <!--end col-->
                                        <div class="col-6 col-sm-3">
                                            <div class="p-3 border border-dashed border-start-0 border-end-0">
                                                <h5 class="mb-1 text-success"><span class="counter-value" data-target="10589">0</span>h</h5>
                                                <p class="text-muted mb-0">Working Hours</p>
                                            </div>
                                        </div>
                                        <!--end col-->
                                    </div>
                                </div>
                                <!-- end card header -->
                                <div class="card-body p-0 pb-2">
                                    <div>
                                        <div id="projects-overview-chart" data-colors='["--vz-primary", "--vz-warning", "--vz-success"]' data-colors-minimal='["--vz-primary", "--vz-primary-rgb, 0.1", "--vz-primary-rgb, 0.50"]' data-colors-interactive='["--vz-primary", "--vz-info", "--vz-warning"]' data-colors-creative='["--vz-secondary", "--vz-warning", "--vz-success"]' data-colors-corporate='["--vz-primary", "--vz-secondary", "--vz-danger"]' data-colors-galaxy='["--vz-primary", "--vz-primary-rgb, 0.1", "--vz-primary-rgb, 0.50"]' data-colors-classic='["--vz-primary", "--vz-secondary", "--vz-warning"]' dir="ltr" class="apex-charts"></div>
                                    </div>
                                </div>
                                <!-- end card body -->
                            </div>
                            <!-- end card -->
                        </div>
                        <!-- end col -->
                    </div>
                    <!-- end row -->

                    <!-- end col -->


                    <!-- end col -->
                </div>
                <!-- end row -->

                <div class="row">
                    <div class="col-xl-7">
                        <div class="card card-height-100">
                            <div class="card-header d-flex align-items-center">
                                <h4 class="card-title flex-grow-1 mb-0">Active Projects</h4>
                                <div class="flex-shrink-0">
                                    <a href="javascript:void(0);" class="btn btn-soft-info btn-sm material-shadow-none">Export Report</a>
                                </div>
                            </div>
                            <!-- end cardheader -->
                            <div class="card-body">
                                <div class="table-responsive table-card">
                                    <table class="table table-nowrap table-centered align-middle">
                                        <thead class="bg-light text-muted">
                                            <tr>
                                                <th scope="col">Project Name</th>
                                                <th scope="col">Project Lead</th>
                                                <th scope="col">Progress</th>
                                                <th scope="col">Assignee</th>
                                                <th scope="col">Status</th>
                                                <th scope="col" style="width: 10%;">Due Date</th>
                                            </tr>
                                            <!-- end tr -->
                                        </thead>
                                        <!-- thead -->

                                        <tbody>
                                            <tr>
                                                <td class="fw-medium">Brand Logo Design</td>
                                                <td>
                                                    <img src="assets/images/users/avatar-1.jpg" class="avatar-xxs rounded-circle me-1 material-shadow" alt="">
                                                    <a href="javascript: void(0);" class="text-reset">Donald Risher</a>
                                                </td>
                                                <td>
                                                    <div class="d-flex align-items-center">
                                                        <div class="flex-shrink-0 me-1 text-muted fs-13">53%</div>
                                                        <div class="progress progress-sm  flex-grow-1" style="width: 68%;">
                                                            <div class="progress-bar bg-primary rounded" role="progressbar" style="width: 53%" aria-valuenow="53" aria-valuemin="0" aria-valuemax="100"></div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="avatar-group flex-nowrap">
                                                        <div class="avatar-group-item">
                                                            <a href="javascript: void(0);" class="d-inline-block">
                                                                <img src="assets/images/users/avatar-1.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                            </a>
                                                        </div>
                                                        <div class="avatar-group-item">
                                                            <a href="javascript: void(0);" class="d-inline-block">
                                                                <img src="assets/images/users/avatar-2.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                            </a>
                                                        </div>
                                                        <div class="avatar-group-item">
                                                            <a href="javascript: void(0);" class="d-inline-block">
                                                                <img src="assets/images/users/avatar-3.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                            </a>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td><span class="badge bg-warning-subtle text-warning">Inprogress</span></td>
                                                <td class="text-muted">06 Sep 2021</td>
                                            </tr>
                                            <!-- end tr -->
                                            <tr>
                                                <td class="fw-medium">Redesign - Landing Page</td>
                                                <td>
                                                    <img src="assets/images/users/avatar-2.jpg" class="avatar-xxs rounded-circle me-1 material-shadow" alt="">
                                                    <a href="javascript: void(0);" class="text-reset">Prezy William</a>
                                                </td>
                                                <td>
                                                    <div class="d-flex align-items-center">
                                                        <div class="flex-shrink-0 text-muted me-1">0%</div>
                                                        <div class="progress progress-sm flex-grow-1" style="width: 68%;">
                                                            <div class="progress-bar bg-primary rounded" role="progressbar" style="width: 0%" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"></div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="avatar-group">
                                                        <div class="avatar-group-item">
                                                            <a href="javascript: void(0);" class="d-inline-block">
                                                                <img src="assets/images/users/avatar-5.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                            </a>
                                                        </div>
                                                        <div class="avatar-group-item">
                                                            <a href="javascript: void(0);" class="d-inline-block">
                                                                <img src="assets/images/users/avatar-6.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                            </a>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td><span class="badge bg-danger-subtle text-danger">Pending</span></td>
                                                <td class="text-muted">13 Nov 2021</td>
                                            </tr>
                                            <!-- end tr -->
                                            <tr>
                                                <td class="fw-medium">Multipurpose Landing Template</td>
                                                <td>
                                                    <img src="assets/images/users/avatar-3.jpg" class="avatar-xxs rounded-circle me-1 material-shadow" alt="">
                                                    <a href="javascript: void(0);" class="text-reset">Boonie Hoynas</a>
                                                </td>
                                                <td>
                                                    <div class="d-flex align-items-center">
                                                        <div class="flex-shrink-0 text-muted me-1">100%</div>
                                                        <div class="progress progress-sm flex-grow-1" style="width: 68%;">
                                                            <div class="progress-bar bg-primary rounded" role="progressbar" style="width: 100%" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100"></div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="avatar-group">
                                                        <div class="avatar-group-item">
                                                            <a href="javascript: void(0);" class="d-inline-block">
                                                                <img src="assets/images/users/avatar-7.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                            </a>
                                                        </div>
                                                        <div class="avatar-group-item">
                                                            <a href="javascript: void(0);" class="d-inline-block">
                                                                <img src="assets/images/users/avatar-8.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                            </a>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td><span class="badge bg-success-subtle text-success">Completed</span></td>
                                                <td class="text-muted">26 Nov 2021</td>
                                            </tr>
                                            <!-- end tr -->
                                            <tr>
                                                <td class="fw-medium">Chat Application</td>
                                                <td>
                                                    <img src="assets/images/users/avatar-5.jpg" class="avatar-xxs rounded-circle me-1 material-shadow" alt="">
                                                    <a href="javascript: void(0);" class="text-reset">Pauline Moll</a>
                                                </td>
                                                <td>
                                                    <div class="d-flex align-items-center">
                                                        <div class="flex-shrink-0 text-muted me-1">64%</div>
                                                        <div class="progress flex-grow-1 progress-sm" style="width: 68%;">
                                                            <div class="progress-bar bg-primary rounded" role="progressbar" style="width: 64%" aria-valuenow="64" aria-valuemin="0" aria-valuemax="100"></div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="avatar-group">
                                                        <div class="avatar-group-item">
                                                            <a href="javascript: void(0);" class="d-inline-block">
                                                                <img src="assets/images/users/avatar-2.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                            </a>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td><span class="badge bg-warning-subtle text-warning">Progress</span></td>
                                                <td class="text-muted">15 Dec 2021</td>
                                            </tr>
                                            <!-- end tr -->
                                            <tr>
                                                <td class="fw-medium">Create Wireframe</td>
                                                <td>
                                                    <img src="assets/images/users/avatar-6.jpg" class="avatar-xxs rounded-circle me-1 material-shadow" alt="">
                                                    <a href="javascript: void(0);" class="text-reset">James Bangs</a>
                                                </td>
                                                <td>
                                                    <div class="d-flex align-items-center">
                                                        <div class="flex-shrink-0 text-muted me-1">77%</div>
                                                        <div class="progress flex-grow-1 progress-sm" style="width: 68%;">
                                                            <div class="progress-bar bg-primary rounded" role="progressbar" style="width: 77%" aria-valuenow="77" aria-valuemin="0" aria-valuemax="100"></div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="avatar-group">
                                                        <div class="avatar-group-item">
                                                            <a href="javascript: void(0);" class="d-inline-block">
                                                                <img src="assets/images/users/avatar-1.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                            </a>
                                                        </div>
                                                        <div class="avatar-group-item">
                                                            <a href="javascript: void(0);" class="d-inline-block">
                                                                <img src="assets/images/users/avatar-6.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                            </a>
                                                        </div>
                                                        <div class="avatar-group-item">
                                                            <a href="javascript: void(0);" class="d-inline-block">
                                                                <img src="assets/images/users/avatar-4.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                            </a>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td><span class="badge bg-warning-subtle text-warning">Progress</span></td>
                                                <td class="text-muted">21 Dec 2021</td>
                                            </tr>
                                            <!-- end tr -->
                                        </tbody>
                                        <!-- end tbody -->
                                    </table>
                                    <!-- end table -->
                                </div>

                                <div class="align-items-center mt-xl-3 mt-4 justify-content-between d-flex">
                                    <div class="flex-shrink-0">
                                        <div class="text-muted">Showing <span class="fw-semibold">5</span> of <span class="fw-semibold">25</span> Results </div>
                                    </div>
                                    <ul class="pagination pagination-separated pagination-sm mb-0">
                                        <li class="page-item disabled">
                                            <a href="#" class="page-link">←</a>
                                        </li>
                                        <li class="page-item">
                                            <a href="#" class="page-link">1</a>
                                        </li>
                                        <li class="page-item active">
                                            <a href="#" class="page-link">2</a>
                                        </li>
                                        <li class="page-item">
                                            <a href="#" class="page-link">3</a>
                                        </li>
                                        <li class="page-item">
                                            <a href="#" class="page-link">→</a>
                                        </li>
                                    </ul>
                                </div>

                            </div>
                            <!-- end card body -->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end col -->

                    <div class="col-xl-5">
                        <div class="card card-height-100">
                            <div class="card-header align-items-center d-flex">
                                <h4 class="card-title mb-0 flex-grow-1 py-1">My Tasks</h4>
                                <div class="flex-shrink-0">
                                    <div class="dropdown card-header-dropdown">
                                        <a class="text-reset dropdown-btn" href="#" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <span class="text-muted">All Tasks <i class="mdi mdi-chevron-down ms-1"></i></span>
                                        </a>
                                        <div class="dropdown-menu dropdown-menu-end">
                                            <a class="dropdown-item" href="#">All Tasks</a>
                                            <a class="dropdown-item" href="#">Completed </a>
                                            <a class="dropdown-item" href="#">Inprogress</a>
                                            <a class="dropdown-item" href="#">Pending</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end card header -->
                            <div class="card-body">
                                <div class="table-responsive table-card">
                                    <table class="table table-borderless table-nowrap table-centered align-middle mb-0">
                                        <thead class="table-light text-muted">
                                            <tr>
                                                <th scope="col">Name</th>
                                                <th scope="col">Dedline</th>
                                                <th scope="col">Status</th>
                                                <th scope="col">Assignee</th>
                                            </tr>
                                        </thead>
                                        <!-- end thead -->
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <div class="form-check">
                                                        <input class="form-check-input fs-15" type="checkbox" value="" id="checkTask1">
                                                        <label class="form-check-label ms-1" for="checkTask1">
                                                            Create new Admin Template
                                                           
                                                        </label>
                                                    </div>
                                                </td>
                                                <td class="text-muted">03 Nov 2021</td>
                                                <td><span class="badge bg-success-subtle text-success">Completed</span></td>
                                                <td>
                                                    <a href="javascript: void(0);" class="d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="" data-bs-original-title="Mary Stoner">
                                                        <img src="assets/images/users/avatar-2.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                    </a>
                                                </td>
                                            </tr>
                                            <!-- end -->
                                            <tr>
                                                <td>
                                                    <div class="form-check">
                                                        <input class="form-check-input fs-15" type="checkbox" value="" id="checkTask2">
                                                        <label class="form-check-label ms-1" for="checkTask2">
                                                            Marketing Coordinator
                                                           
                                                        </label>
                                                    </div>
                                                </td>
                                                <td class="text-muted">17 Nov 2021</td>
                                                <td><span class="badge bg-warning-subtle text-warning">Progress</span></td>
                                                <td>
                                                    <a href="javascript: void(0);" class="d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="" data-bs-original-title="Den Davis">
                                                        <img src="assets/images/users/avatar-7.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                    </a>
                                                </td>
                                            </tr>
                                            <!-- end -->
                                            <tr>
                                                <td>
                                                    <div class="form-check">
                                                        <input class="form-check-input fs-15" type="checkbox" value="" id="checkTask3">
                                                        <label class="form-check-label ms-1" for="checkTask3">
                                                            Administrative Analyst
                                                           
                                                        </label>
                                                    </div>
                                                </td>
                                                <td class="text-muted">26 Nov 2021</td>
                                                <td><span class="badge bg-success-subtle text-success">Completed</span></td>
                                                <td>
                                                    <a href="javascript: void(0);" class="d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="" data-bs-original-title="Alex Brown">
                                                        <img src="assets/images/users/avatar-6.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                    </a>
                                                </td>
                                            </tr>
                                            <!-- end -->
                                            <tr>
                                                <td>
                                                    <div class="form-check">
                                                        <input class="form-check-input fs-15" type="checkbox" value="" id="checkTask4">
                                                        <label class="form-check-label ms-1" for="checkTask4">
                                                            E-commerce Landing Page
                                                           
                                                        </label>
                                                    </div>
                                                </td>
                                                <td class="text-muted">10 Dec 2021</td>
                                                <td><span class="badge bg-danger-subtle text-danger">Pending</span></td>
                                                <td>
                                                    <a href="javascript: void(0);" class="d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="" data-bs-original-title="Prezy Morin">
                                                        <img src="assets/images/users/avatar-5.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                    </a>
                                                </td>
                                            </tr>
                                            <!-- end -->
                                            <tr>
                                                <td>
                                                    <div class="form-check">
                                                        <input class="form-check-input fs-15" type="checkbox" value="" id="checkTask5">
                                                        <label class="form-check-label ms-1" for="checkTask5">
                                                            UI/UX Design
                                                           
                                                        </label>
                                                    </div>
                                                </td>
                                                <td class="text-muted">22 Dec 2021</td>
                                                <td><span class="badge bg-warning-subtle text-warning">Progress</span></td>
                                                <td>
                                                    <a href="javascript: void(0);" class="d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="" data-bs-original-title="Stine Nielsen">
                                                        <img src="assets/images/users/avatar-1.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                    </a>
                                                </td>
                                            </tr>
                                            <!-- end -->
                                            <tr>
                                                <td>
                                                    <div class="form-check">
                                                        <input class="form-check-input fs-15" type="checkbox" value="" id="checkTask6">
                                                        <label class="form-check-label ms-1" for="checkTask6">
                                                            Projects Design
                                                           
                                                        </label>
                                                    </div>
                                                </td>
                                                <td class="text-muted">31 Dec 2021</td>
                                                <td><span class="badge bg-danger-subtle text-danger">Pending</span></td>
                                                <td>
                                                    <a href="javascript: void(0);" class="d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="" data-bs-original-title="Jansh William">
                                                        <img src="assets/images/users/avatar-4.jpg" alt="" class="rounded-circle avatar-xxs material-shadow">
                                                    </a>
                                                </td>
                                            </tr>
                                            <!-- end -->
                                        </tbody>
                                        <!-- end tbody -->
                                    </table>
                                    <!-- end table -->
                                </div>
                                <div class="mt-3 text-center">
                                    <a href="javascript:void(0);" class="text-muted text-decoration-underline">Load More</a>
                                </div>
                            </div>
                            <!-- end card body -->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end col -->
                </div>
                <!-- end row -->

                <div class="row">
                    <div class="col-xxl-4">
                        <div class="card card-height-100">
                            <div class="card-header align-items-center d-flex">
                                <h4 class="card-title mb-0 flex-grow-1">Team Members</h4>
                                <div class="flex-shrink-0">
                                    <div class="dropdown card-header-dropdown">
                                        <a class="text-reset dropdown-btn" href="#" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <span class="fw-semibold text-uppercase fs-12">Sort by: </span><span class="text-muted">Last 30 Days<i class="mdi mdi-chevron-down ms-1"></i></span>
                                        </a>
                                        <div class="dropdown-menu dropdown-menu-end">
                                            <a class="dropdown-item" href="#">Today</a>
                                            <a class="dropdown-item" href="#">Yesterday</a>
                                            <a class="dropdown-item" href="#">Last 7 Days</a>
                                            <a class="dropdown-item" href="#">Last 30 Days</a>
                                            <a class="dropdown-item" href="#">This Month</a>
                                            <a class="dropdown-item" href="#">Last Month</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end card header -->

                            <div class="card-body">

                                <div class="table-responsive table-card">
                                    <table class="table table-borderless table-nowrap align-middle mb-0">
                                        <thead class="table-light text-muted">
                                            <tr>
                                                <th scope="col">Member</th>
                                                <th scope="col">Hours</th>
                                                <th scope="col">Tasks</th>
                                                <th scope="col">Status</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td class="d-flex">
                                                    <img src="assets/images/users/avatar-1.jpg" alt="" class="avatar-xs rounded-3 me-2 material-shadow">
                                                    <div>
                                                        <h5 class="fs-13 mb-0">Donald Risher</h5>
                                                        <p class="fs-12 mb-0 text-muted">Product Manager</p>
                                                    </div>
                                                </td>
                                                <td>
                                                    <h6 class="mb-0">110h : <span class="text-muted">150h</span></h6>
                                                </td>
                                                <td>258
                                                </td>
                                                <td style="width: 5%;">
                                                    <div id="radialBar_chart_1" data-colors='["--vz-primary"]' data-chart-series="50" class="apex-charts" dir="ltr"></div>
                                                </td>
                                            </tr>
                                            <!-- end tr -->
                                            <tr>
                                                <td class="d-flex">
                                                    <img src="assets/images/users/avatar-2.jpg" alt="" class="avatar-xs rounded-3 me-2 material-shadow">
                                                    <div>
                                                        <h5 class="fs-13 mb-0">Jansh Brown</h5>
                                                        <p class="fs-12 mb-0 text-muted">Lead Developer</p>
                                                    </div>
                                                </td>
                                                <td>
                                                    <h6 class="mb-0">83h : <span class="text-muted">150h</span></h6>
                                                </td>
                                                <td>105
                                                </td>
                                                <td>
                                                    <div id="radialBar_chart_2" data-colors='["--vz-primary"]' data-chart-series="45" class="apex-charts" dir="ltr"></div>
                                                </td>
                                            </tr>
                                            <!-- end tr -->
                                            <tr>
                                                <td class="d-flex">
                                                    <img src="assets/images/users/avatar-7.jpg" alt="" class="avatar-xs rounded-3 me-2 material-shadow">
                                                    <div>
                                                        <h5 class="fs-13 mb-0">Carroll Adams</h5>
                                                        <p class="fs-12 mb-0 text-muted">Lead Designer</p>
                                                    </div>
                                                </td>
                                                <td>
                                                    <h6 class="mb-0">58h : <span class="text-muted">150h</span></h6>
                                                </td>
                                                <td>75
                                                </td>
                                                <td>
                                                    <div id="radialBar_chart_3" data-colors='["--vz-primary"]' data-chart-series="75" class="apex-charts" dir="ltr"></div>
                                                </td>
                                            </tr>
                                            <!-- end tr -->
                                            <tr>
                                                <td class="d-flex">
                                                    <img src="assets/images/users/avatar-4.jpg" alt="" class="avatar-xs rounded-3 me-2 material-shadow">
                                                    <div>
                                                        <h5 class="fs-13 mb-0">William Pinto</h5>
                                                        <p class="fs-12 mb-0 text-muted">UI/UX Designer</p>
                                                    </div>
                                                </td>
                                                <td>
                                                    <h6 class="mb-0">96h : <span class="text-muted">150h</span></h6>
                                                </td>
                                                <td>85
                                                </td>
                                                <td>
                                                    <div id="radialBar_chart_4" data-colors='["--vz-warning"]' data-chart-series="25" class="apex-charts" dir="ltr"></div>
                                                </td>
                                            </tr>
                                            <!-- end tr -->
                                            <tr>
                                                <td class="d-flex">
                                                    <img src="assets/images/users/avatar-6.jpg" alt="" class="avatar-xs rounded-3 me-2 material-shadow">
                                                    <div>
                                                        <h5 class="fs-13 mb-0">Garry Fournier</h5>
                                                        <p class="fs-12 mb-0 text-muted">Web Designer</p>
                                                    </div>
                                                </td>
                                                <td>
                                                    <h6 class="mb-0">76h : <span class="text-muted">150h</span></h6>
                                                </td>
                                                <td>69
                                                </td>
                                                <td>
                                                    <div id="radialBar_chart_5" data-colors='["--vz-primary"]' data-chart-series="60" class="apex-charts" dir="ltr"></div>
                                                </td>
                                            </tr>
                                            <!-- end tr -->
                                            <tr>
                                                <td class="d-flex">
                                                    <img src="assets/images/users/avatar-5.jpg" alt="" class="avatar-xs rounded-3 me-2 material-shadow">
                                                    <div>
                                                        <h5 class="fs-13 mb-0">Susan Denton</h5>
                                                        <p class="fs-12 mb-0 text-muted">Lead Designer</p>
                                                    </div>
                                                </td>

                                                <td>
                                                    <h6 class="mb-0">123h : <span class="text-muted">150h</span></h6>
                                                </td>
                                                <td>658
                                                </td>
                                                <td>
                                                    <div id="radialBar_chart_6" data-colors='["--vz-success"]' data-chart-series="85" class="apex-charts" dir="ltr"></div>
                                                </td>
                                            </tr>
                                            <!-- end tr -->
                                            <tr>
                                                <td class="d-flex">
                                                    <img src="assets/images/users/avatar-3.jpg" alt="" class="avatar-xs rounded-3 me-2 material-shadow">
                                                    <div>
                                                        <h5 class="fs-13 mb-0">Joseph Jackson</h5>
                                                        <p class="fs-12 mb-0 text-muted">React Developer</p>
                                                    </div>
                                                </td>
                                                <td>
                                                    <h6 class="mb-0">117h : <span class="text-muted">150h</span></h6>
                                                </td>
                                                <td>125
                                                </td>
                                                <td>
                                                    <div id="radialBar_chart_7" data-colors='["--vz-primary"]' data-chart-series="70" class="apex-charts" dir="ltr"></div>
                                                </td>
                                            </tr>
                                            <!-- end tr -->
                                        </tbody>
                                        <!-- end tbody -->
                                    </table>
                                    <!-- end table -->
                                </div>
                            </div>
                            <!-- end cardbody -->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end col -->

                    <div class="col-xxl-4 col-lg-6">
                        <div class="card card-height-100">
                            <div class="card-header align-items-center d-flex">
                                <h4 class="card-title mb-0 flex-grow-1">Chat</h4>
                                <div class="flex-shrink-0">
                                    <div class="dropdown card-header-dropdown">
                                        <a class="text-reset dropdown-btn" href="#" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <span class="text-muted"><i class="ri-settings-4-line align-bottom me-1"></i>Setting <i class="mdi mdi-chevron-down ms-1"></i></span>
                                        </a>
                                        <div class="dropdown-menu dropdown-menu-end">
                                            <a class="dropdown-item" href="#"><i class="ri-user-2-fill align-bottom text-muted me-2"></i>View Profile</a>
                                            <a class="dropdown-item" href="#"><i class="ri-inbox-archive-line align-bottom text-muted me-2"></i>Archive</a>
                                            <a class="dropdown-item" href="#"><i class="ri-mic-off-line align-bottom text-muted me-2"></i>Muted</a>
                                            <a class="dropdown-item" href="#"><i class="ri-delete-bin-5-line align-bottom text-muted me-2"></i>Delete</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end card header -->

                            <div class="card-body p-0">
                                <div id="users-chat">
                                    <div class="chat-conversation p-3" id="chat-conversation" data-simplebar style="height: 400px;">
                                        <ul class="list-unstyled chat-conversation-list chat-sm" id="users-conversation">
                                            <li class="chat-list left">
                                                <div class="conversation-list">
                                                    <div class="chat-avatar">
                                                        <img src="assets/images/users/avatar-2.jpg" alt="" class="material-shadow">
                                                    </div>
                                                    <div class="user-chat-content">
                                                        <div class="ctext-wrap">
                                                            <div class="ctext-wrap-content">
                                                                <p class="mb-0 ctext-content">Good morning 😊</p>
                                                            </div>
                                                            <div class="dropdown align-self-start message-box-drop">
                                                                <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                    <i class="ri-more-2-fill"></i>
                                                                </a>
                                                                <div class="dropdown-menu">
                                                                    <a class="dropdown-item" href="#"><i class="ri-reply-line me-2 text-muted align-bottom"></i>Reply</a>
                                                                    <a class="dropdown-item" href="#"><i class="ri-share-line me-2 text-muted align-bottom"></i>Forward</a>
                                                                    <a class="dropdown-item" href="#"><i class="ri-file-copy-line me-2 text-muted align-bottom"></i>Copy</a>
                                                                    <a class="dropdown-item" href="#"><i class="ri-bookmark-line me-2 text-muted align-bottom"></i>Bookmark</a>
                                                                    <a class="dropdown-item delete-item" href="#"><i class="ri-delete-bin-5-line me-2 text-muted align-bottom"></i>Delete</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="conversation-name"><small class="text-muted time">09:07 am</small> <span class="text-success check-message-icon"><i class="ri-check-double-line align-bottom"></i></span></div>
                                                    </div>
                                                </div>
                                            </li>
                                            <!-- chat-list -->

                                            <li class="chat-list right">
                                                <div class="conversation-list">
                                                    <div class="user-chat-content">
                                                        <div class="ctext-wrap">
                                                            <div class="ctext-wrap-content">
                                                                <p class="mb-0 ctext-content">Good morning, How are you? What about our next meeting?</p>
                                                            </div>
                                                            <div class="dropdown align-self-start message-box-drop">
                                                                <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                    <i class="ri-more-2-fill"></i>
                                                                </a>
                                                                <div class="dropdown-menu">
                                                                    <a class="dropdown-item" href="#"><i class="ri-reply-line me-2 text-muted align-bottom"></i>Reply</a>
                                                                    <a class="dropdown-item" href="#"><i class="ri-share-line me-2 text-muted align-bottom"></i>Forward</a>
                                                                    <a class="dropdown-item" href="#"><i class="ri-file-copy-line me-2 text-muted align-bottom"></i>Copy</a>
                                                                    <a class="dropdown-item" href="#"><i class="ri-bookmark-line me-2 text-muted align-bottom"></i>Bookmark</a>
                                                                    <a class="dropdown-item delete-item" href="#"><i class="ri-delete-bin-5-line me-2 text-muted align-bottom"></i>Delete</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="conversation-name"><small class="text-muted time">09:08 am</small> <span class="text-success check-message-icon"><i class="ri-check-double-line align-bottom"></i></span></div>
                                                    </div>
                                                </div>
                                            </li>
                                            <!-- chat-list -->

                                            <li class="chat-list left">
                                                <div class="conversation-list">
                                                    <div class="chat-avatar">
                                                        <img src="assets/images/users/avatar-2.jpg" alt="" class="material-shadow">
                                                    </div>
                                                    <div class="user-chat-content">
                                                        <div class="ctext-wrap">
                                                            <div class="ctext-wrap-content">
                                                                <p class="mb-0 ctext-content">Yeah everything is fine. Our next meeting tomorrow at 10.00 AM</p>
                                                            </div>
                                                            <div class="dropdown align-self-start message-box-drop">
                                                                <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                    <i class="ri-more-2-fill"></i>
                                                                </a>
                                                                <div class="dropdown-menu">
                                                                    <a class="dropdown-item" href="#"><i class="ri-reply-line me-2 text-muted align-bottom"></i>Reply</a>
                                                                    <a class="dropdown-item" href="#"><i class="ri-share-line me-2 text-muted align-bottom"></i>Forward</a>
                                                                    <a class="dropdown-item" href="#"><i class="ri-file-copy-line me-2 text-muted align-bottom"></i>Copy</a>
                                                                    <a class="dropdown-item" href="#"><i class="ri-bookmark-line me-2 text-muted align-bottom"></i>Bookmark</a>
                                                                    <a class="dropdown-item delete-item" href="#"><i class="ri-delete-bin-5-line me-2 text-muted align-bottom"></i>Delete</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="ctext-wrap">
                                                            <div class="ctext-wrap-content">
                                                                <p class="mb-0 ctext-content">Hey, I'm going to meet a friend of mine at the department store. I have to buy some presents for my parents 🎁.</p>
                                                            </div>
                                                            <div class="dropdown align-self-start message-box-drop">
                                                                <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                    <i class="ri-more-2-fill"></i>
                                                                </a>
                                                                <div class="dropdown-menu">
                                                                    <a class="dropdown-item" href="#"><i class="ri-reply-line me-2 text-muted align-bottom"></i>Reply</a>
                                                                    <a class="dropdown-item" href="#"><i class="ri-share-line me-2 text-muted align-bottom"></i>Forward</a>
                                                                    <a class="dropdown-item" href="#"><i class="ri-file-copy-line me-2 text-muted align-bottom"></i>Copy</a>
                                                                    <a class="dropdown-item" href="#"><i class="ri-bookmark-line me-2 text-muted align-bottom"></i>Bookmark</a>
                                                                    <a class="dropdown-item delete-item" href="#"><i class="ri-delete-bin-5-line me-2 text-muted align-bottom"></i>Delete</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="conversation-name"><small class="text-muted time">09:10 am</small> <span class="text-success check-message-icon"><i class="ri-check-double-line align-bottom"></i></span></div>
                                                    </div>
                                                </div>
                                            </li>
                                            <!-- chat-list -->

                                            <li class="chat-list right">
                                                <div class="conversation-list">
                                                    <div class="user-chat-content">
                                                        <div class="ctext-wrap">
                                                            <div class="ctext-wrap-content">
                                                                <p class="mb-0 ctext-content">Wow that's great</p>
                                                            </div>
                                                            <div class="dropdown align-self-start message-box-drop">
                                                                <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                    <i class="ri-more-2-fill"></i>
                                                                </a>
                                                                <div class="dropdown-menu">
                                                                    <a class="dropdown-item" href="#"><i class="ri-reply-line me-2 text-muted align-bottom"></i>Reply</a>
                                                                    <a class="dropdown-item" href="#"><i class="ri-share-line me-2 text-muted align-bottom"></i>Forward</a>
                                                                    <a class="dropdown-item" href="#"><i class="ri-file-copy-line me-2 text-muted align-bottom"></i>Copy</a>
                                                                    <a class="dropdown-item" href="#"><i class="ri-bookmark-line me-2 text-muted align-bottom"></i>Bookmark</a>
                                                                    <a class="dropdown-item delete-item" href="#"><i class="ri-delete-bin-5-line me-2 text-muted align-bottom"></i>Delete</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="conversation-name"><small class="text-muted time">09:12 am</small> <span class="text-success check-message-icon"><i class="ri-check-double-line align-bottom"></i></span></div>
                                                    </div>
                                                </div>
                                            </li>
                                            <!-- chat-list -->

                                            <li class="chat-list left">
                                                <div class="conversation-list">
                                                    <div class="chat-avatar">
                                                        <img src="assets/images/users/avatar-2.jpg" alt="" class="material-shadow">
                                                    </div>
                                                    <div class="user-chat-content">
                                                        <div class="ctext-wrap">
                                                            <div class="message-img mb-0">
                                                                <div class="message-img-list">
                                                                    <div>
                                                                        <a class="popup-img d-inline-block" href="assets/images/small/img-1.jpg">
                                                                            <img src="assets/images/small/img-1.jpg" alt="" class="rounded border">
                                                                        </a>
                                                                    </div>
                                                                    <div class="message-img-link">
                                                                        <ul class="list-inline mb-0">
                                                                            <li class="list-inline-item dropdown">
                                                                                <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                    <i class="ri-more-fill"></i>
                                                                                </a>
                                                                                <div class="dropdown-menu">
                                                                                    <a class="dropdown-item" href="assets/images/small/img-1.jpg" download=""><i class="ri-download-2-line me-2 text-muted align-bottom"></i>Download</a>
                                                                                    <a class="dropdown-item" href="#"><i class="ri-reply-line me-2 text-muted align-bottom"></i>Reply</a>
                                                                                    <a class="dropdown-item" href="#"><i class="ri-share-line me-2 text-muted align-bottom"></i>Forward</a>
                                                                                    <a class="dropdown-item" href="#"><i class="ri-bookmark-line me-2 text-muted align-bottom"></i>Bookmark</a>
                                                                                    <a class="dropdown-item delete-item" href="#"><i class="ri-delete-bin-5-line me-2 text-muted align-bottom"></i>Delete</a>
                                                                                </div>
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                </div>

                                                                <div class="message-img-list">
                                                                    <div>
                                                                        <a class="popup-img d-inline-block" href="assets/images/small/img-2.jpg">
                                                                            <img src="assets/images/small/img-2.jpg" alt="" class="rounded border">
                                                                        </a>
                                                                    </div>
                                                                    <div class="message-img-link">
                                                                        <ul class="list-inline mb-0">
                                                                            <li class="list-inline-item dropdown">
                                                                                <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                    <i class="ri-more-fill"></i>
                                                                                </a>
                                                                                <div class="dropdown-menu">
                                                                                    <a class="dropdown-item" href="assets/images/small/img-2.jpg" download=""><i class="ri-download-2-line me-2 text-muted align-bottom"></i>Download</a>
                                                                                    <a class="dropdown-item" href="#"><i class="ri-reply-line me-2 text-muted align-bottom"></i>Reply</a>
                                                                                    <a class="dropdown-item" href="#"><i class="ri-share-line me-2 text-muted align-bottom"></i>Forward</a>
                                                                                    <a class="dropdown-item" href="#"><i class="ri-bookmark-line me-2 text-muted align-bottom"></i>Bookmark</a>
                                                                                    <a class="dropdown-item delete-item" href="#"><i class="ri-delete-bin-5-line me-2 text-muted align-bottom"></i>Delete</a>
                                                                                </div>
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="conversation-name"><small class="text-muted time">09:30 am</small> <span class="text-success check-message-icon"><i class="ri-check-double-line align-bottom"></i></span></div>
                                                    </div>
                                                </div>
                                            </li>
                                            <!-- chat-list -->
                                        </ul>
                                    </div>
                                </div>
                                <div class="border-top border-top-dashed">
                                    <div class="row g-2 mx-3 mt-2 mb-3">
                                        <div class="col">
                                            <div class="position-relative">
                                                <input type="text" class="form-control border-light bg-light" placeholder="Enter Message...">
                                            </div>
                                        </div>
                                        <!-- end col -->
                                        <div class="col-auto">
                                            <button type="submit" class="btn btn-info"><span class="d-none d-sm-inline-block me-2">Send</span> <i class="mdi mdi-send float-end"></i></button>
                                        </div>
                                        <!-- end col -->
                                    </div>
                                    <!-- end row -->
                                </div>
                            </div>
                            <!-- end cardbody -->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end col -->

                    <div class="col-xxl-4 col-lg-6">
                        <div class="card card-height-100">
                            <div class="card-header align-items-center d-flex">
                                <h4 class="card-title mb-0 flex-grow-1">Projects Status</h4>
                                <div class="flex-shrink-0">
                                    <div class="dropdown card-header-dropdown">
                                        <a class="dropdown-btn text-muted" href="#" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">All Time <i class="mdi mdi-chevron-down ms-1"></i>
                                        </a>
                                        <div class="dropdown-menu dropdown-menu-end">
                                            <a class="dropdown-item" href="#">All Time</a>
                                            <a class="dropdown-item" href="#">Last 7 Days</a>
                                            <a class="dropdown-item" href="#">Last 30 Days</a>
                                            <a class="dropdown-item" href="#">Last 90 Days</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end card header -->

                            <div class="card-body">
                                <div id="prjects-status" data-colors='["--vz-success", "--vz-primary", "--vz-warning", "--vz-danger"]' data-colors-minimal='["--vz-primary", "--vz-primary-rgb, 0.85", "--vz-primary-rgb, 0.70", "--vz-primary-rgb, 0.50"]' data-colors-galaxy='["--vz-primary", "--vz-primary-rgb, 0.85", "--vz-primary-rgb, 0.70", "--vz-primary-rgb, 0.50"]' class="apex-charts" dir="ltr"></div>
                                <div class="mt-3">
                                    <div class="d-flex justify-content-center align-items-center mb-4">
                                        <h2 class="me-3 ff-secondary mb-0">258</h2>
                                        <div>
                                            <p class="text-muted mb-0">Total Projects</p>
                                            <p class="text-success fw-medium mb-0">
                                                <span class="badge bg-success-subtle text-success p-1 rounded-circle"><i class="ri-arrow-right-up-line"></i></span>+3 New
                                               
                                            </p>
                                        </div>
                                    </div>

                                    <div class="d-flex justify-content-between border-bottom border-bottom-dashed py-2">
                                        <p class="fw-medium mb-0"><i class="ri-checkbox-blank-circle-fill text-success align-middle me-2"></i>Completed</p>
                                        <div>
                                            <span class="text-muted pe-5">125 Projects</span>
                                            <span class="text-success fw-medium fs-12">15870hrs</span>
                                        </div>
                                    </div>
                                    <!-- end -->
                                    <div class="d-flex justify-content-between border-bottom border-bottom-dashed py-2">
                                        <p class="fw-medium mb-0"><i class="ri-checkbox-blank-circle-fill text-primary align-middle me-2"></i>In Progress</p>
                                        <div>
                                            <span class="text-muted pe-5">42 Projects</span>
                                            <span class="text-success fw-medium fs-12">243hrs</span>
                                        </div>
                                    </div>
                                    <!-- end -->
                                    <div class="d-flex justify-content-between border-bottom border-bottom-dashed py-2">
                                        <p class="fw-medium mb-0"><i class="ri-checkbox-blank-circle-fill text-warning align-middle me-2"></i>Yet to Start</p>
                                        <div>
                                            <span class="text-muted pe-5">58 Projects</span>
                                            <span class="text-success fw-medium fs-12">~2050hrs</span>
                                        </div>
                                    </div>
                                    <!-- end -->
                                    <div class="d-flex justify-content-between py-2">
                                        <p class="fw-medium mb-0"><i class="ri-checkbox-blank-circle-fill text-danger align-middle me-2"></i>Cancelled</p>
                                        <div>
                                            <span class="text-muted pe-5">89 Projects</span>
                                            <span class="text-success fw-medium fs-12">~900hrs</span>
                                        </div>
                                    </div>
                                    <!-- end -->
                                </div>
                            </div>
                            <!-- end cardbody -->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end col -->
                </div>
                <!-- end row -->
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
    <!-- JAVASCRIPT -->
    <script src="../NewDashboard/js/bootstrap.bundle.min.js"></script>
    <script src="../NewDashboard/js/simplebar.min.js"></script>
    <script src="../NewDashboard/js/waves.min.js"></script>
    <script src="../NewDashboard/js/feather.min.js"></script>
    <script src="../NewDashboard/js/lord-icon-2.1.0.js"></script>
    <script src="../NewDashboard/js/plugins.js"></script>

    <!-- apexcharts -->
    <script src="../NewDashboard/js/apexcharts.min.js"></script>

    <!-- projects js -->
    <script src="../NewDashboard/js/dashboard-projects.init.js"></script>

    <!-- App js -->
    <script src="../NewDashboard/js/app.js"></script>
</asp:Content>
