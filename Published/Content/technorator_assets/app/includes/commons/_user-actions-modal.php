<div class="modal fade user-action-modal" id="user_modal" tabindex="-1" role="dialog" aria-labelledby="user_modalLabel" aria-hidden="true">
	<div class="modal-dialog" role="document">
		<div class="modal-content">
			<div class="modal-body">
				<button type="button" class="btn-close-modal" data-dismiss="modal" aria-label="Close">
					<span class="btn-icon" aria-hidden="true">&times;</span>
				</button>
				<div class="user-actions-container">
					<div class="actions-inner">
						
						<div class="actions-toggler-container">
							<ul class="nav nav-tabs" id="user_atns_tabs" role="tablist">
								<li class="nav-item">
									<a href="#tab_login" class="nav-link active" data-toggle="tab" role="tab">Login</a>
								</li>
								<li class="nav-item">
									<a href="#tab_register" class="nav-link" data-toggle="tab" role="tab">Register</a>
								</li>
							</ul>
						</div>
						<div class="actions-content-container tab-content" hidden-->
							<div class="tab-pane fade in active" id="tab_login" role="tabpanel">
								<div class="user-form-container">
									<div class="form-inner">
										<form action="#" class="global-form">
											<div class="form-group">
												<div class="input-group">
													<input type="email" name="login_email" id="login_email" class="form-control" placeholder="Email"/>
													<span class="group-addon"><span class="fa fa-envelope"></span></span>
												</div>
											</div>
											<div class="form-group">
												<div class="input-group">
													<input type="password" name="login_password" id="login_password" class="form-control" placeholder="Password"/>
													<span class="group-addon"><span class="fa fa-lock"></span></span>
												</div>
											</div>
											<div class="form-group form-checkbox">
												<label class="form-checkbox-label">
													<input type="checkbox" name="remember_me" class="remember_me"/>
													<span class="btn-text">Remember Me</span>
												</label>
											</div>
											<div class="btn-submit">
												<button class="btn btn-primary btn-block btn-float">
													<span class="btn-inner">
														<span class="btn-text">Login</span>
														<span class="btn-icon"><span class="fa fa-long-arrow-right"></span></span>
													</span>
												</button>
											</div>
										</form>
									</div>
								</div>
							</div>
							<div class="tab-pane fade" id="tab_register" role="tabpanel">
								<div class="user-form-container">
									<div class="form-inner">
										<form action="#" class="global-form">
											<div class="form-group">
												<div class="input-group">
													<input type="email" name="register_email" id="register_email" class="form-control" placeholder="Email"/>
													<span class="group-addon"><span class="fa fa-envelope"></span></span>
												</div>
											</div>
											<div class="form-group">
												<div class="input-group">
													<input type="password" name="register_password" id="register_password" class="form-control" placeholder="Password"/>
													<span class="group-addon"><span class="fa fa-lock"></span></span>
												</div>
											</div>
											<div class="form-group">
												<div class="input-group">
													<input type="password" name="register_repeat_password" id="register_repeat_password" class="form-control" placeholder="Repeat Password"/>
													<span class="group-addon"><span class="fa fa-lock"></span></span>
												</div>
											</div>
											<div class="form-group form-checkbox">
												<label class="form-checkbox-label">
													<input type="checkbox" name="register_agree_terms" class="register_agree_terms"/>
													<span class="btn-text">I Agree terms &amp; condition</span>
												</label>
											</div>
											<div class="btn-submit">
												<button class="btn btn-success btn-block btn-float">
													<span class="btn-inner">
														<span class="btn-text">Register</span>
														<span class="btn-icon"><span class="fa fa-long-arrow-right"></span></span>
													</span>
												</button>
											</div>
										</form>
									</div>
								</div>
							</div>
						</div>

					</div>
				</div>
			</div>
		</div>
	</div>
</div>